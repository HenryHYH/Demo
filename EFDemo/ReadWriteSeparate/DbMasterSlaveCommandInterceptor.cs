using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
            UpdateToMaster(interceptionContext);
            base.NonQueryExecuting(command, interceptionContext);
        }

        private void UpdateToMaster(DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var context in interceptionContext.DbContexts)
            {
                UpdateConnectionStringIfNeed(context.Database.Connection, MasterConnectionString);
            }
        }

        private void UpdateToSlave(DbCommand command, DbInterceptionContext interceptionContext)
        {
            var isDistributedTran = null != Transaction.Current &&
                                    Transaction.Current.TransactionInformation.Status != TransactionStatus.Committed;
            foreach (var context in interceptionContext.DbContexts)
            {
                var isDbTran = null != context.Database.CurrentTransaction;
                var isCommandTran = null != command.Transaction;
                var isTrans = isDistributedTran || isDbTran || isCommandTran;

                var connectionString = isTrans ?
                                       MasterConnectionString :
                                       SlaveConnectionString;

                UpdateConnectionStringIfNeed(context.Database.Connection, connectionString);
            }
        }

        private void UpdateConnectionStringIfNeed(DbConnection connection, string connectionString)
        {
            if (!ConnectionStringCompare(connection, connectionString))
            {
                Console.WriteLine("A.ConnStr = " + connection.ConnectionString);
                Console.WriteLine("B.ConnStr = " + connectionString);
                UpdateConnectionString(connection, connectionString);
            }
        }

        public static void UpdateConnectionString(DbConnection connection, string connectionString)
        {
            var state = connection.State;
            var isOpened = state == ConnectionState.Open;
            if (isOpened)
                connection.Close();

            connection.ConnectionString = connectionString;

            if (isOpened)
                connection.Open();
        }

        public static bool ConnectionStringCompare(DbConnection connection, string connectionString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            var first = factory.CreateConnectionStringBuilder();
            first.ConnectionString = connection.ConnectionString;

            var second = factory.CreateConnectionStringBuilder();
            second.ConnectionString = connectionString;

            var result = first["Data Source"].Equals(second["Data Source"]);
            Console.WriteLine("EquivalentTo " + result.ToString());

            return result;
        }
    }
}
