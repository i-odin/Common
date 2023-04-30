using Common.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public class StringBuilderWrapper
{
    //TODO: сделать private
    protected readonly StringBuilder _sb;
    public StringBuilderWrapper(StringBuilder sb)
        => _sb = sb;

    protected StringBuilderWrapper AppendNewLine(string value)
    {
        if (_sb.Length > 0)
            _sb.Append("\r\n");
        _sb.Append(value);
        return this;
    }
    
    protected StringBuilderWrapper AppendString(string value)
    {
        _sb.Append("'");
        _sb.Append(value);
        _sb.Append("'");
        return this;
    }

    protected StringBuilderWrapper AppendNull()
    {
        _sb.Append("null");
        return this;
    }

    public StringBuilderWrapper BracketLeft()
    {
        _sb.Append("(");
        return this;
    }

    public string BracketRitht()
    {
        var result = ")";
        _sb.Append(result);
        return result;
    }

    public StringBuilderWrapper InsertBracketRitht(int index)
    {
        _sb.Insert(index, BracketRitht());
        return this;
    }

    public StringBuilderWrapper Or()
    {
        _sb.Append(" or ");
        return this;
    }

    public StringBuilderWrapper And()
    {
        _sb.Append(" and ");
        return this;
    }

    public StringBuilderWrapper NotEqual()
    {
        _sb.Append(" <> ");
        return this;
    }

    public StringBuilderWrapper Equal()
    {
        _sb.Append(" = ");
        return this;
    }

    public string Comma()
    {
        var result = ", ";
        _sb.Append(result);
        return result;
    }
    public StringBuilderWrapper InsertComma(int index)
    {
        _sb.Insert(index, Comma());
        return this;
    }
    public override string ToString() => _sb.ToString();
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

    public Translator<T> NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
    {
        Field(field).NotEqual();
        if (value is null)
            AppendNull();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

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

    public Translator<T> Field<TField>(Expression<Func<T, TField>> field)
    {
        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        _sb.Append(member.Name);
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
        _sb.Append(value.ToString());
        return this;
    }
}
