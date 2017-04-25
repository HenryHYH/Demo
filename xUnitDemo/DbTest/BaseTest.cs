using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Reflection;

namespace DbTest
{
    public abstract class BaseTest
    {
        protected DbContext context;

        public BaseTest()
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            context = new MyDbContext(GetTestDbName());
            context.Database.Delete();
            context.Database.Create();
        }

        protected string GetTestDbName()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return string.Format(@"Data Source={0}\DbTest.sdf;Persist Security Info=False", path);
        }

        protected T SaveAndLoadEntity<T>(T entity, bool disposeContext = true)
            where T : BaseEntity
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();

            long id = entity.Id;

            if (disposeContext)
            {
                context.Dispose();
                context = new MyDbContext(GetTestDbName());
            }

            var fromDb = context.Set<T>().Find(id);
            return fromDb;
        }
    }
}
