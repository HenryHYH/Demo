using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace ReadWriteSeparate
{
    public class DbMasterSlaveConnectionInterceptor : EmptyConnectionInterceptor
    {
        private Lazy<string> masterConnectionString = new Lazy<string>(() =>
                                ConfigurationManager.ConnectionStrings["Master"].ConnectionString);

        public string MasterConnectionString
        {
            get { return masterConnectionString.Value; }
        }

        public override void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            UpdateConnectionStringIfNeed(connection, MasterConnectionString);
        }

        public override void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            UpdateConnectionStringIfNeed(connection, MasterConnectionString);
        }

        private void UpdateConnectionStringIfNeed(DbConnection connection, string connectionString)
        {
            if (!DbMasterSlaveCommandInterceptor.ConnectionStringCompare(connection, connectionString))
            {
                DbMasterSlaveCommandInterceptor.UpdateConnectionString(connection, connectionString);
            }
        }
    }
}
