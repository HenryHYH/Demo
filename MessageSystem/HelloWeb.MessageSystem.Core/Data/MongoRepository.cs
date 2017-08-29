using HelloWeb.MessageSystem.Core.Domain;
using HelloWeb.MessageSystem.Core.Setting;
using Repository.Mongo;

namespace HelloWeb.MessageSystem.Core.Data
{
    public class MongoRepository<T> : Repository<T>, IBaseRepository<T>
         where T : BaseEntity
    {
        public MongoRepository(SystemSetting setting)
            : base(setting.MongoConnectionString, typeof(T).Name)
        {
        }
    }
}
