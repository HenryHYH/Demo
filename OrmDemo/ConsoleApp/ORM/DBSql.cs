using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp.ORM
{
    public class DBSql : IDataBase
    {
        private const string CONNECTION_STRING = @"Data Source=192.168.111.20;User ID=sa;Password=HWu7f8nH;Initial Catalog=Test;";

        public IList<T> FindAs<T>(Expression<Func<T, bool>> lambdawhere)
        {
            string sql = string.Format("SELECT * FROM [{0}] WITH(NOLOCK)", typeof(T).Name);
            IDictionary<string, object> parameter = null;
            if (lambdawhere != null)
            {
                var resolve = new ResolveExpression();
                resolve.ResolveToSql(lambdawhere);
                sql = string.Format("{0} WHERE {1}", sql, resolve.SqlWhere);
                parameter = resolve.Parameters;
            }

            IList<T> list = null;
            using (IDbConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                list = conn.Query<T>(sql, new DynamicParameters(parameter)).ToList();
            }

            return list;
        }

        public int Remove<T>(Expression<Func<T, bool>> lambdawhere)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Source<T>()
        {
            return new SqlQuery<T>();
        }
    }
}
