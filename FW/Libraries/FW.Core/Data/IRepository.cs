namespace FW.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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

        void Delete(Expression<Func<T, bool>> expression);

        T GetById(int id);

        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        #endregion Methods
    }
}