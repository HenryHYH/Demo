using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ORM
{
    public class SqlProvider<T> : IQueryProvider
    {
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new SqlQuery<TElement>(expression, this);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            MethodCallExpression methodCall = expression as MethodCallExpression;
            Expression<Func<T, bool>> result = null;
            while (methodCall != null)
            {
                Expression method = methodCall.Arguments[0];
                Expression lambda = methodCall.Arguments[1];
                LambdaExpression right = (lambda as UnaryExpression).Operand as LambdaExpression;
                if (typeof(IOrderedQueryable<T>) == methodCall.Type)
                {

                }
                else
                {
                    if (result == null)
                        result = Expression.Lambda<Func<T, bool>>(right.Body, right.Parameters);
                    else
                    {
                        Expression left = (result as LambdaExpression).Body;
                        Expression temp = Expression.And(right.Body, left);
                        result = Expression.Lambda<Func<T, bool>>(temp, result.Parameters);
                    }
                }

                methodCall = method as MethodCallExpression;
            }

            var source = new DBSql().FindAs<T>(result);
            dynamic _temp = source;
            TResult t = (TResult)_temp;

            return t;
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
