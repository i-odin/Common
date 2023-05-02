using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IUpdateTranslator<T>
        where T : class
{
    IUpdateTranslator<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
}

public class UpdateTranslator<T> : Translator<T>, IUpdateTranslator<T>
    where T : class
{
    public UpdateTranslator(StringBuilder sb) : base(sb) { }

    public static implicit operator UpdateTranslator<T>(StringBuilder sb)
        => (UpdateTranslator<T>)new UpdateTranslator<T>(sb).Update();

    IUpdateTranslator<T> IUpdateTranslator<T>.Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        Set(field, value); return this;
    }
}