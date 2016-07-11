using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Datas;
using ConsoleApp.Domain.Models;
using MongoDB.Driver;

namespace ConsoleApp.Services.Users
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IMongoCollection<User> collection;

        #endregion

        #region Ctor

        public UserService()
        {
            this.collection = MongoDBProvider.GetCollection<User>("mongodb://localhost:27017", "Test");
        }

        #endregion

        #region Methods

        public IEnumerable<User> Get()
        {
            var list = collection.Find(x => x.Age > 1)
                                 .ToList();

            return list;
        }

        public void Insert(User entity)
        {
            collection.InsertOne(entity);
        }

        #endregion
    }
}
