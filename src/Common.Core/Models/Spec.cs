using Common.Core.Extensions;
using Common.Core.Utilities;
using System.Linq.Expressions;

namespace Common.Core.Models;

//TODO Test
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