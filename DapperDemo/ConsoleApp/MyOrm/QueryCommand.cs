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

        #region Methods

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

        public IList<T> ToList()
        {
            var sql = new StringBuilder();
            sql.AppendFormat("SELECT * FROM [{0}] [t] WITH(NOLOCK)", typeof(T).Name);
            sql.AppendLine();

            IDictionary<string, object> parameters = null;
            IList<T> list = new List<T>();

            if (whereConds.Any())
            {
                var exp = whereConds[0];
                for (int i = 1, iMax = whereConds.Count; i < iMax; i++)
                    exp = exp.And(whereConds[i]);

                var result = new ExpressionAnalyzer(exp).GetAnalysisResult();
                var str = string.Join(" ", result.StackList);
                sql.AppendLine("WHERE " + str);
                parameters = result.ParamList;
            }
            if (orderByConds.Any())
            {
                var str = string.Join(", ", orderByConds.Select(x => string.Format("{0} {1}", x.Key, x.Value)));
                sql.AppendLine("ORDER BY " + str);
            }

            using (IDbConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                var param = new DynamicParameters(parameters);
                list = conn.Query<T>(sql.ToString(), param).ToList();
            }

            return list;
        }

        #endregion
    }
}
