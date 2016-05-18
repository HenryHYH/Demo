using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace UPCHINA.EntityFramework.Repositories
{
    public abstract class UPCHINARepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<UPCHINADbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected UPCHINARepositoryBase(IDbContextProvider<UPCHINADbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class UPCHINARepositoryBase<TEntity> : UPCHINARepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected UPCHINARepositoryBase(IDbContextProvider<UPCHINADbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
