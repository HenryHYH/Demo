using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Demo.EntityFramework.Repositories
{
    public abstract class DemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected DemoRepositoryBase(IDbContextProvider<DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class DemoRepositoryBase<TEntity> : DemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DemoRepositoryBase(IDbContextProvider<DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
