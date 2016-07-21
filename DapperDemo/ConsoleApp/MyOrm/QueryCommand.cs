using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp.MyOrm
{
    public class QueryCommand<T>
    {
        #region Fields

        private static readonly string CONNECTION_STRING = "Data Source=192.168.111.20;Initial Catalog=Test;User ID=sa;Password=HWu7f8nH;";

        private readonly IList<Expression<Func<T, bool>>> whereConds = new List<Expression<Func<T, bool>>>();
        private readonly IDictionary<string, string> orderByConds = new Dictionary<string, string>();
        private int? skip = null;
        private int? take = null;

        #endregion

        #region Ctor

        public QueryCommand()
        {
        }

        #endregion

        #region Condition methods

        public QueryCommand<T> Where(Expression<Func<T, bool>> func)
        {
            whereConds.Add(func);

            return this;
        }

        public QueryCommand<T> OrderBy(Expression<Func<T, object>> func)
        {
            var result = new ExpressionAnalyzer(func).GetAnalysisResult();
            var field = string.Join("", result.StackList);
            if (!orderByConds.ContainsKey(field))
                orderByConds.Add(field, "ASC");

            return this;
        }

        public QueryCommand<T> OrderByDescding(Expression<Func<T, object>> func)
        {
            var result = new ExpressionAnalyzer(func).GetAnalysisResult();
            var field = string.Join("", result.StackList);
            if (!orderByConds.ContainsKey(field))
                orderByConds.Add(field, "DESC");

            return this;
        }

        public QueryCommand<T> Skip(int count)
        {
            skip = count;

            return this;
        }

        public QueryCommand<T> Take(int count)
        {
            take = count;

            return this;
        }

        #endregion

        #region Execute methods

        public IList<T> ToList()
        {
            var command = GetSelectCommand();
            var sql = command.Item1;
            var parameters = command.Item2;

            var list = new List<T>();
            using (IDbConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                var param = new DynamicParameters(parameters);
                list = conn.Query<T>(sql, param).ToList();
            }

            return list;
        }

        public bool Any()
        {
            var command = GetSelectCommand(withoutOrderBy: true);
            var sql = command.Item1;
            var parameters = command.Item2;

            sql = string.Format("SELECT TOP 1 1 FROM ({0}{1}) [t_any]", Environment.NewLine, sql);
            var param = new DynamicParameters(parameters);

            var result = false;
            using (IDbConnection conn = new SqlConnection(CONNECTION_STRING))
                result = conn.Query<bool>(sql, param).FirstOrDefault();

            return result;
        }

        public int Count(Expression<Func<T, object>> func)
        {
            return Aggr<int>(func, "COUNT", isCastType: false);
        }

        public int CountDistinct(Expression<Func<T, object>> func)
        {
            return Aggr<int>(func, "COUNT", isCastType: false, isDistinct: true);
        }

        public TResult Sum<TResult>(Expression<Func<T, object>> func)
        {
            return Aggr<TResult>(func, "SUM");
        }

        public TResult Max<TResult>(Expression<Func<T, object>> func)
        {
            return Aggr<TResult>(func, "MAX");
        }

        public TResult Min<TResult>(Expression<Func<T, object>> func)
        {
            return Aggr<TResult>(func, "MIN");
        }

        public TResult Avg<TResult>(Expression<Func<T, object>> func)
        {
            return Aggr<TResult>(func, "AVG");
        }

        #endregion

        #region Private methods

        private TResult Aggr<TResult>(Expression<Func<T, object>> func, string aggrType, bool isCastType = true, bool isDistinct = false)
        {
            var command = GetAggrCommand<TResult>(func, aggrType, isCastType, isDistinct);
            var sql = command.Item1;
            var parameters = command.Item2;

            TResult data = default(TResult);
            using (IDbConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                var param = new DynamicParameters(parameters);
                data = conn.QueryFirst<TResult>(sql, param);
            }

            return data;
        }

        private string CastType(Type type)
        {
            string result = "";

            if (typeof(float) == type ||
                typeof(double) == type ||
                typeof(decimal) == type)
                result = "decimal";
            else if (typeof(short) == type ||
                    typeof(int) == type ||
                    typeof(long) == type)
                result = "int";

            return result;
        }

        private Tuple<string, IDictionary<string, object>> GetAggrCommand<TResult>(Expression<Func<T, object>> func, string aggrType, bool isCastType, bool isDistinct)
        {
            var analysisResult = new ExpressionAnalyzer(func).GetAnalysisResult();
            var field = string.Join("", analysisResult.StackList).Replace("[t]", "[t_aggr]");

            var typeName = typeof(TResult).Name;

            var command = GetSelectCommand(true);
            var sql = command.Item1;
            var parameters = command.Item2;
            var castType = CastType(typeof(TResult));

            if (isCastType)
                field = string.Format("CAST({0} AS {1})", field, castType);
            if (isDistinct)
                field = string.Format("DISTINCT {0}", field);

            sql = string.Format("SELECT {0}({1}) FROM (\r\n{2}) [t_aggr]", aggrType, field, sql);

            return new Tuple<string, IDictionary<string, object>>(sql, parameters);
        }

        public Tuple<string, IDictionary<string, object>> GetSelectCommand(bool withoutOrderBy = false)
        {
            var sqlBuilder = new StringBuilder();
            IDictionary<string, object> parameters = null;

            // SELECT FROM
            var tableName = typeof(T).Name;
            var selectFrom = string.Format("SELECT * FROM [{0}] [t] WITH(NOLOCK)", tableName);
            sqlBuilder.AppendLine(selectFrom);

            // WHERE
            if (whereConds.Any())
            {
                var exp = whereConds[0];
                for (int i = 1, iMax = whereConds.Count; i < iMax; i++)
                    exp = exp.And(whereConds[i]);

                var result = new ExpressionAnalyzer(exp).GetAnalysisResult();
                var str = string.Join(" ", result.StackList);
                sqlBuilder.AppendLine("WHERE " + str);
                parameters = result.ParamList;
            }

            // ORDER BY
            var orderBy = "ORDER BY [t].[Id]";
            if (orderByConds.Any())
            {
                var str = string.Join(", ", orderByConds.Select(x => string.Format("{0} {1}", x.Key, x.Value)));
                orderBy = "ORDER BY " + str;
            }

            // PAGER
            if (take.HasValue || skip.HasValue)
            {
                if (take.HasValue)
                    sqlBuilder.Insert(0, string.Format("SELECT TOP {0} * FROM (", take.Value) + Environment.NewLine);
                else
                    sqlBuilder.Insert(0, "SELECT * FROM (" + Environment.NewLine);
                sqlBuilder.AppendLine(") [t_row]");

                if (skip.HasValue)
                {
                    sqlBuilder.Replace(selectFrom, string.Format("SELECT *, ROW_NUMBER() OVER ({0}) AS ROW_NUM FROM [{1}] [t] WITH(NOLOCK)", orderBy, tableName));
                    sqlBuilder.AppendLine(string.Format("WHERE [t_row].[ROW_NUM] > {0}", skip.Value));
                }
            }
            else if (!withoutOrderBy)
                sqlBuilder.AppendLine(orderBy);

            return new Tuple<string, IDictionary<string, object>>(sqlBuilder.ToString(), parameters);
        }

        #endregion
    }
}
