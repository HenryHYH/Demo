namespace FW.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T>
        where T : BaseEntity
    {
        #region Properties

        IQueryable<T> Table
        {
            get;
        }

        #endregion Properties

        #region Methods

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        T GetById(int id);

        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        #endregion Methods
    }
}