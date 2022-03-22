using Common.Core.Utilities;
using Common.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Core.Tests
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Comments { get; set; }
        public bool IsForSell { get; set; }
        public int InStock { get; set; }
        
        public static Spec<Order> IsStockExpretion => new Spec<Order>(x => x.InStock > 0);
        public static Spec<Order> IsForSellExpretion => new Spec<Order>(x => x.IsForSell);
    }

    internal class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }

    public class TestSpec
    {
        private readonly DbContextOptions<MyAppContext> dbContextOptions;

        public TestSpec()
        {
            dbContextOptions = new DbContextOptionsBuilder<MyAppContext>()
                .UseInMemoryDatabase(databaseName: "MyBlogDb")
                .Options;
        }

        [Fact]
        public void test()
        {
            var myAppContext = new MyAppContext(dbContextOptions);
            myAppContext.Orders.Add(new Order() { Number = 1, Comments = "Новый-1", IsForSell = true });
            myAppContext.Orders.Add(new Order() { Number = 2, Comments = "Новый-2", InStock = 10, IsForSell = true });
            myAppContext.Orders.Add(new Order() { Number = 3, Comments = "Новый-3", InStock = 10 });

            myAppContext.SaveChanges();

            var qwe = myAppContext.Orders.Where(Order.IsForSellExpretion).ToList();

            //Func<Order, bool> IsAvvereble = x => x.InStock > 0 && x.IsForSell;
            //Expression<Func<Order, bool>> IsAvvereble = x => x.InStock > 0 && x.IsForSell;
            //IsAvvereble.ToFunc();
            //CompiledExpressions<Order, bool>.ToFunc(IsAvvereble);
            var qwe1 = myAppContext.Orders.Where(Order.IsForSellExpretion && Order.IsStockExpretion).ToList();
            //var qwe1 = myAppContext.Orders.Where(x => x.IsAvvereble).ToList();

        }
    }

    public class Spec<T>
    {
        readonly Expression<Func<T, bool>> _expr;
        Func<T, bool> _func;

        public Spec(Expression<Func<T, bool>> expr)
        {
            Throw.NotNull(expr);
            _expr = expr;
        }

        public bool IsSatisfiedBy(T obj) => (_func ?? (_func = _expr.ToFunc()))(obj);

        public Spec<TParent> From<TParent>(Expression<Func<TParent, T>> mapFrom)
            => _expr.From(mapFrom);

        public static bool operator false(Spec<T> _) => false;

        public static bool operator true(Spec<T> _) => false;

        public static Spec<T> operator &(Spec<T> spec1, Spec<T> spec2)
            => new Spec<T>(spec1._expr.And(spec2._expr));

        public static Spec<T> operator |(Spec<T> spec1, Spec<T> spec2)
            => new Spec<T>(spec1._expr.Or(spec2._expr));

        public static Spec<T> operator !(Spec<T> spec)
            => new Spec<T>(spec._expr.Not());

        public static implicit operator Expression<Func<T, bool>>(Spec<T> spec)
            => spec._expr;

        public static implicit operator Spec<T>(Expression<Func<T, bool>> expression)
            => new Spec<T>(expression);
    }

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

    internal class CompiledExpressions<TIn, TOut>
    {
        static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> _cache 
            = new ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>>();

        internal static Func<TIn, TOut> ToFunc(Expression<Func<TIn, TOut>> expr) => 
            _cache.GetOrAdd(expr, x=>x.Compile());
    }
}
