using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace ReadWriteSeparate
{
    public class DbMasterSlaveConnectionInterceptor : IDbConnectionInterceptor
    {
        public void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            Console.WriteLine("BeganTransaction + " + connection.ConnectionString);
        }

        public void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
        {
            Console.WriteLine("BeginningTransaction = " + connection.ConnectionString);
        }

        public void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Closed = " + connection.ConnectionString);
        }

        public void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Closing = " + connection.ConnectionString);
        }

        public void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ConnectionStringGetting = " + connection.ConnectionString);
        }

        public void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ConnectionStringGot = " + connection.ConnectionString);
        }

        public void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ConnectionStringSet = " + connection.ConnectionString);
        }

        public void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ConnectionStringSetting = " + connection.ConnectionString);
        }

        public void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {
            Console.WriteLine("ConnectionTimeoutGetting = " + connection.ConnectionString);
        }

        public void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
        {
            Console.WriteLine("ConnectionTimeoutGot = " + connection.ConnectionString);
        }

        public void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("DatabaseGetting = " + connection.ConnectionString);
        }

        public void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("DatabaseGot = " + connection.ConnectionString);
        }

        public void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("DataSourceGetting = " + connection.ConnectionString);
        }

        public void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("DataSourceGot = " + connection.ConnectionString);
        }

        public void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Disposed = " + connection.ConnectionString);
        }

        public void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Disposing = " + connection.ConnectionString);
        }

        public void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {
            Console.WriteLine("EnlistedTransaction = " + connection.ConnectionString);
        }

        public void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
        {
            Console.WriteLine("EnlistingTransaction = " + connection.ConnectionString);
        }

        public void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Opened = " + connection.ConnectionString);
        }

        public void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
            Console.WriteLine("Opening = " + connection.ConnectionString);

            interceptionContext.DbContexts.First();
        }

        public void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ServerVersionGetting = " + connection.ConnectionString);
        }

        public void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
        {
            Console.WriteLine("ServerVersionGot = " + connection.ConnectionString);
        }

        public void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            Console.WriteLine("StateGetting = " + connection.ConnectionString);
        }

        public void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
        {
            Console.WriteLine("StateGot = " + connection.ConnectionString);
        }
    }
}
