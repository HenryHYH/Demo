using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ORM
{
    public interface IDataBase
    {
        IList<T> FindAs<T>(Expression<Func<T, bool>> lambdawhere);

        int Remove<T>(Expression<Func<T, bool>> lambdawhere);

        IQueryable<T> Source<T>();
    }
}
