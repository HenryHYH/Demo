using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ORM
{
    public class SqlQuery<T> : IQueryable<T>, IOrderedQueryable<T>
    {
        private Expression _expression;
        private IQueryProvider _provider;

        public SqlQuery()
        {
            _provider = new SqlProvider<T>();
            _expression = Expression.Constant(this);
        }

        public SqlQuery(Expression expression, IQueryProvider provider)
        {
            _expression = expression;
            _provider = provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var result = _provider.Execute<List<T>>(_expression);
            if (result == null)
                yield break;
            foreach (var item in result)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Type ElementType
        {
            get { return typeof(SqlQuery<T>); }
        }

        public Expression Expression
        {
            get { return _expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }
    }
}
