using System;
using System.Linq.Expressions;

namespace Ises.Core.Utils
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, TR>> AddOrAssign<T, TR>(this Expression<Func<T, TR>> initialExp, Expression<Func<T, TR>> additionalExp)
        {
            initialExp = (initialExp == null) ? additionalExp : initialExp.And(additionalExp);
            return initialExp;
        }

        private static Expression<Func<TParam, TResult>> And<TParam, TResult>(this Expression<Func<TParam, TResult>> expr1, Expression<Func<TParam, TResult>> expr2)
        {
            var orExpr = Expression.AndAlso(expr1.Body, GetBody(expr2, expr1.Parameters[0]));

            return Expression.Lambda<Func<TParam, TResult>>(orExpr, expr1.Parameters);
        }

        private static Expression GetBody<TParam, TResult>(Expression<Func<TParam, TResult>> exp, ParameterExpression param)
        {
            var visitor = new ParameterUpdateVisitor(exp.Parameters[0], param);
            return visitor.Visit(exp.Body);
        }

        private class ParameterUpdateVisitor : ExpressionVisitor
        {
            private ParameterExpression OldParameter
            {
                get; set;
            }

            private ParameterExpression NewParameter
            {
                get; set;
            }

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                OldParameter = oldParameter;
                NewParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (ReferenceEquals(node, OldParameter))
                {
                    return NewParameter;
                }

                return base.VisitParameter(node);
            }
        }
    }
}
