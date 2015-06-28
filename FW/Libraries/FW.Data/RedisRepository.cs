using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Core;
using FW.Core.Data;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace FW.Data
{
    public partial class RedisRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly DataSettings dataSettings;

        private IRedisClient client;
        private IRedisTypedClient<T> redisTypedClient;
        private IRedisList<T> table;

        #endregion

        #region Ctor

        public RedisRepository(DataSettings dataSettings)
        {
            this.dataSettings = dataSettings;

            client = new RedisClient(dataSettings.RawDataSettings["RedisHost"]);
            redisTypedClient = client.As<T>();
            table = redisTypedClient.Lists[typeof(T).Name];
        }

        #endregion

        #region Methods

        public IQueryable<T> Table
        {
            get { return table.AsQueryable(); }
        }

        public void Delete(T entity)
        {
            if (null != entity)
            {
                redisTypedClient.RemoveItemFromList(table, entity);
            }
        }

        public T GetById(object id)
        {
            return table.Where(x => x.Id == (int)id).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            if (null != entity)
            {
                redisTypedClient.AddItemToList(table, entity);
                client.Save();
            }
        }

        public void Update(T entity)
        {
            if (null != entity)
            {
                var old = GetById(entity.Id);
                if (null != old)
                {
                    redisTypedClient.RemoveItemFromList(table, old);
                    redisTypedClient.AddItemToList(table, entity);
                    client.Save();
                }
            }
        }

        #endregion
    }
}
