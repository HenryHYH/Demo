using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Transactions;

namespace ReadWriteSeparate
{
    public class DbMasterSlaveCommandInterceptor : DbCommandInterceptor
    {
        private Lazy<string> masterConnectionString = new Lazy<string>(() =>
                                ConfigurationManager.ConnectionStrings["Master"].ConnectionString);
        private Lazy<string> slaveConnectionString = new Lazy<string>(() =>
                                ConfigurationManager.ConnectionStrings["Slave"].ConnectionString);
        private int count = 0;

        public string MasterConnectionString
        {
            get { return masterConnectionString.Value; }
        }

        public string SlaveConnectionString
        {
            get { return slaveConnectionString.Value; }
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Console.WriteLine();
            Console.WriteLine("Reader");
            Console.WriteLine("SQL = " + command.CommandText);
            UpdateToSlave(command, interceptionContext);
            base.ReaderExecuting(command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Console.WriteLine();
            Console.WriteLine("Scalar");
            UpdateToSlave(command, interceptionContext);
            base.ScalarExecuting(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Console.WriteLine();
            Console.WriteLine("NonQuery");
            UpdateToMaster(command, interceptionContext);
            base.NonQueryExecuting(command, interceptionContext);
        }

        private void UpdateToMaster(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var context in interceptionContext.DbContexts)
            {
                //Console.WriteLine("Master, " + masterConnectionString);
                UpdateConnectionStringIfNeed(context, context.Database.Connection, MasterConnectionString, "172.17.22.18", "Master");
            }
        }

        private void UpdateToSlave(DbCommand command, DbInterceptionContext interceptionContext)
        {
            bool isDistributedTran = null != Transaction.Current &&
                                     Transaction.Current.TransactionInformation.Status != TransactionStatus.Committed;
            foreach (var context in interceptionContext.DbContexts)
            {
                bool isDbTran = null != context.Database.CurrentTransaction;
                var isTrans = isDistributedTran || isDbTran;

                var connectionString = isTrans ?
                                       MasterConnectionString :
                                       SlaveConnectionString;

                var dataSource = isTrans ?
                                 "172.17.22.18" :
                                 "10.1.20.97";

                var configName = isTrans ?
                                 "Master" :
                                 "Slave";

                //Console.WriteLine("Salve, " + connectionString);
                UpdateConnectionStringIfNeed(context, context.Database.Connection, connectionString, dataSource, configName);
            }
        }

        private void UpdateConnectionStringIfNeed(DbContext context,
                                                  DbConnection connection,
                                                  string connectionString,
                                                  string dataSource,
                                                  string configName)
        {
            if (!ConnectionStringCompare(connection, connectionString))
            {
                //Console.WriteLine("cnt = " + (++count));
                Console.WriteLine("A = " + connection.ConnectionString);
                Console.WriteLine("B = " + connectionString);
                UpdateConnectionString(context, connection, connectionString, dataSource, configName);
            }
        }

        private void UpdateConnectionString(DbContext context,
                                            DbConnection connection,
                                            string connectionString,
                                            string dataSource,
                                            string configName)
        {
            var state = connection.State;
            var isOpened = state == ConnectionState.Open;
            if (isOpened)
                connection.Close();

            connection.ConnectionString = connectionString;
            //context.ChangeDatabase(dataSource: dataSource,
            //                       configConnectionStringName: configName);

            if (isOpened)
                connection.Open();
        }

        private bool ConnectionStringCompare(DbConnection connection, string connectionString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            var first = factory.CreateConnectionStringBuilder();
            first.ConnectionString = connection.ConnectionString;
            //Console.WriteLine("first = " + first.ConnectionString);

            var second = factory.CreateConnectionStringBuilder();
            second.ConnectionString = connectionString;
            //Console.WriteLine("second = " + second.ConnectionString);

            var result = first["Data Source"].Equals(second["Data Source"]);
            Console.WriteLine("EquivalentTo " + result.ToString());

            return result;
        }
    }
}
