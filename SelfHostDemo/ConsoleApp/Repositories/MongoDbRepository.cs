using System.Linq;
using ConsoleApp.Core.Datas;
using ConsoleApp.Core.Domain;
using ConsoleApp.Core.Settings;
using MongoDB.Driver;

namespace ConsoleApp.Repositories
{
    public class MongoDbRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region Fields

        private readonly DataSettings dataSettings;
        private readonly IMongoCollection<T> collection;

        #endregion

        #region Ctor

        public MongoDbRepository(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;
            this.collection = MongoDBProvider.GetCollection<T>(dataSettings);
        }

        #endregion

        #region Properties

        public IQueryable<T> Table
        {
            get
            {
                return collection.AsQueryable();
            }
        }

        #endregion

        #region Methods

        public void Add(T entity)
        {
            collection.InsertOne(entity);
        }

        public void Modify(T entity)
        {
            collection.ReplaceOne(x => x.Id == entity.Id, entity);
        }

        public void Delete(string id)
        {
            collection.DeleteOne(x => x.Id == id);
        }

        #endregion
    }
}
