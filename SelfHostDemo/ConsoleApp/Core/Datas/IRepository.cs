using System.Linq;

namespace ConsoleApp.Core.Datas
{
    public interface IRepository<T>
    {
        #region Properties

        IQueryable<T> Table { get; }

        #endregion

        #region Methods

        void Add(T entity);

        void Modify(T entity);

        void Delete(string id);

        #endregion
    }
}
