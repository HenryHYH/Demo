﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Core.Caching;
using ConsoleApp.Core.Datas;
using ConsoleApp.Core.Domain.Users;
using ConsoleApp.Core.Settings;
using MongoDB.Driver;

namespace ConsoleApp.Services.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IMongoCollection<User> collection;
        private readonly ICacheManager cacheManager;

        #endregion

        #region Ctor

        public UserService(DataSettings dataSettings, ICacheManager cacheManager)
        {
            this.collection = MongoDBProvider.GetCollection<User>(dataSettings);
            this.cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IEnumerable<User> Get()
        {
            var list = collection.AsQueryable()
                                 .Where(x => x.Name.Contains("Test"));

            return list.ToList();
        }

        public void Insert(User entity)
        {
            collection.InsertOne(entity);
        }

        #endregion
    }
}
