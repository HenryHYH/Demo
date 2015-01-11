namespace FW.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core;
    using FW.Core.Data;
    using FW.Core.Infrastructure;

    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    using MongoDBIntIdGenerator;

    public partial class MongoRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region Fields

        private MongoCollection<T> collection;
        private Settings settings;

        #endregion Fields

        #region Constructors

        public MongoRepository(Settings settings)
        {
            this.settings = settings;

            var client = new MongoClient(settings.GetSetting("ConnectionString"));
            var server = client.GetServer();

            var database = server.GetDatabase(settings.GetSetting("DatabaseName"));

            if (!database.CollectionExists(typeof(T).Name))
            {
                database.CreateCollection(typeof(T).Name);
            }
            this.collection = database.GetCollection<T>(typeof(T).Name);

            BsonSerializer.RegisterIdGenerator(typeof(int), IntIdGenerator.Instance);
        }

        #endregion Constructors

        #region Properties

        public IQueryable<T> Table
        {
            get
            {
                return collection.AsQueryable<T>();
            }
        }

        #endregion Properties

        #region Methods

        public void Delete(T entity)
        {
            collection.Remove(Query<T>.EQ(x => x.Id, entity.Id));
        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return collection.FindOne(Query<T>.EQ(x => x.Id, id));
        }

        public void Insert(T entity)
        {
            collection.Insert(entity);
        }

        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            collection.Save(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}