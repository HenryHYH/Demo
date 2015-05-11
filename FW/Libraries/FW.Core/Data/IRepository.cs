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

        T GetById(object id);

        void Insert(T entity);

        void Update(T entity);

        #endregion Methods
    }
}