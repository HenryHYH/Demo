using HelloWeb.MessageSystem.Core.Domain;
using Repository.Mongo;

namespace HelloWeb.MessageSystem.Core.Data
{
    public interface IBaseRepository<T> : IRepository<T>
          where T : BaseEntity
    {
    }
}
