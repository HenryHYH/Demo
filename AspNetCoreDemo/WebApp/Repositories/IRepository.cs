using System.Collections.Generic;

namespace WebApp.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
    }
}
