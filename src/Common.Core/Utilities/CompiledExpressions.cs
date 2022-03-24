using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Common.Core.Utilities
{
    //TODO Test
    internal class CompiledExpressions<TIn, TOut>
    {
        static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> _cache
            = new ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>>();

        internal static Func<TIn, TOut> ToFunc(Expression<Func<TIn, TOut>> expr) =>
            _cache.GetOrAdd(expr, x => x.Compile());
    }
}
