using Common.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Core.Extensions
{
    //TODO Test
    public static class ExpressionExtensions
    {
        public static Func<TIn, TOut> ToFunc<TIn, TOut>(this Expression<Func<TIn, TOut>> expr) =>
            CompiledExpressions<TIn, TOut>.ToFunc(expr);

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) =>
            first.Compose<Func<T, bool>>(second, Expression.AndAlso);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) =>
            first.Compose<Func<T, bool>>(second, Expression.OrElse);

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression) =>
            Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);

        public static Expression<T> Compose<T>(this LambdaExpression first, LambdaExpression second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<TDestination, TReturn>> From<TSource, TDestination, TReturn>(
            this Expression<Func<TSource, TReturn>> source, Expression<Func<TDestination, TSource>> mapFrom)
            => Expression.Lambda<Func<TDestination, TReturn>>(
                Expression.Invoke(source, mapFrom.Body), mapFrom.Parameters);

        private class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) =>
                new ParameterRebinder(map).Visit(exp);

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(p, out replacement))
                    p = replacement;

                return base.VisitParameter(p);
            }
        }
    }
}
