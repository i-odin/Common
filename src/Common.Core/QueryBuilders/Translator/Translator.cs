using Common.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public class StringBuilderWrapper
{
    protected StringBuilder _sb;
    public StringBuilderWrapper(StringBuilder sb)
        => _sb = sb;

    protected StringBuilderWrapper AppendNewLine(string value)
    {
        if (_sb.Length > 0)
            _sb.Append("\r\n");
        _sb.Append(value);
        return this;
    }
    protected StringBuilderWrapper Append(string value)
    {
        _sb.Append(value);
        return this;
    }

    protected StringBuilderWrapper AppendString(string value)
    {
        Append("'");
        Append(value);
        Append("'");
        return this;
    }

    protected StringBuilderWrapper AppendNull()
    {
        Append("null");
        return this;
    }
}

public class Translator<T> : StringBuilderWrapper
     where T : class
{
    private Type _typeEntity;
    protected Type typeEntity
    {
        get
        {
            if (_typeEntity == null) _typeEntity = typeof(T);
            return _typeEntity;
        }
    }
    public Translator(StringBuilder sb) : base(sb) { }

    public Translator<T> BracketLeft()
    {
        Append("(");
        return this;
    }

    public Translator<T> BracketRitht()
    {
        Append(")");
        return this;
    }

    public Translator<T> Or()
    {
        Append(" or ");
        return this;
    }

    public Translator<T> And()
    {
        Append(" and ");
        return this;
    }

    public Translator<T> NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
    {
        Field(field).NotEqual();
        if (value is null)
            AppendNull();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> NotEqual()
    {
        Append(" <> ");
        return this;
    }

    public Translator<T> Equal<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
    {
        Field(field).Equal();
        if (value is null)
            AppendNull();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> Equal()
    {
        Append(" = ");
        return this;
    }

    public Translator<T> Field<TField>(Expression<Func<T, TField>> field)
    {
        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        Append(member.Name);
        return this;
    }

    public Translator<T> Comma()
    {
        Append(", ");
        return this;
    }

    public Translator<T> Value(string value)
    {
        if (value != null) AppendString(value);
        else AppendNull();
        return this;
    }

    public Translator<T> Value(Guid value)
    {
        if (value != null) AppendString(value.ToString());
        else AppendNull();
        return this;
    }

    public Translator<T> Value(DateTime? value)
    {
        if (value.HasValue) Value(value.Value);
        else AppendNull();
        return this;
    }

    public Translator<T> Value(DateTime value)
    {
        AppendString(value.ToStringIso8601());
        return this;
    }

    public Translator<T> Value(int? value)
    {
        if (value.HasValue) Value(value.Value);
        else AppendNull();
        return this;
    }

    public Translator<T> Value(int value)
    {
        Append(value.ToString());
        return this;
    }
}
