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

    public UpdateTranslator<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        if (_isComma) Comma();
        else _isComma = true;

        Field(field).Equal();
        Value(value);
        return this;
    }

    public UpdateTranslator<T> Update()
    {
        AppendNewLine("update ").Append(typeEntity.Name).AppendNewLine("set ");
        return this;
    }
    public static UpdateTranslator<T> Update(StringBuilder sb, Action<UpdateTranslator<T>> inner)
    {
        var obj = new UpdateTranslator<T>(sb).Update();
        inner(obj);
        return obj;
    }
    public static implicit operator UpdateTranslator<T>(StringBuilder sb)
        => new UpdateTranslator<T>(sb);

    IUpdateTranslator<T> IUpdateTranslator<T>.Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        Set(field, value); return this;
    }
}