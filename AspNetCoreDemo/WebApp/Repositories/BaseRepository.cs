using System.Collections.Generic;

namespace WebApp.Repositories
{
    public class BaseRepository<T> : IRepository<T>
    {
        public IEnumerable<T> Get()
        {
            return new List<T> { default(T) };
        }
    }
}
