using FW.Core;
using FW.Core.Data;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDBIntIdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data
{
    public partial class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private MongoDatabase database;
        private MongoCollection<T> collection;

        #endregion

        #region Ctor

        public MongoRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            this.database = server.GetDatabase("Test");

            if (!database.CollectionExists(typeof(T).Name))
            {
                database.CreateCollection(typeof(T).Name);
            }
            this.collection = database.GetCollection<T>(typeof(T).Name);

            BsonSerializer.RegisterIdGenerator(typeof(int), IntIdGenerator.Instance);
        }

        #endregion

        #region Methods

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

        public void Delete(T entity)
        {
            collection.Remove(Query<T>.EQ(x => x.Id, entity.Id));


        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        public IQueryable<T> Table
        {
            get
            {
                return collection.AsQueryable<T>();
            }
        }

        #endregion
    }
}
