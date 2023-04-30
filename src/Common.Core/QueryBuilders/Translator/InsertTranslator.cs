using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IInsertTranslator<T>
    where T : class
{
    IInsertTranslator<T> Values<TField>(Expression<Func<T, TField>> field, TField value);
}

public class InsertTranslator<T> : Translator<T>, IInsertTranslator<T>
    where T : class
{
    private int _indexInsert;
    private bool _isComma;
    public InsertTranslator(StringBuilder sb) : base(sb) { }
    public InsertTranslator<T> Insert()
    {
        AppendNewLine("insert into ");
        _sb.Append(typeEntity.Name);
        _sb.Append(" ");
        BracketLeft();
        _indexInsert = _sb.Length;

        AppendNewLine("values ");
        BracketLeft();
        return this;
    }

    public InsertTranslator<T> InsertEnd()
    {
        InsertBracketRitht(_indexInsert);
        return this;
    }

    public InsertTranslator<T> Values<TField>(Expression<Func<T, TField>> field, TField value)
    {
        if (_isComma)
        {
            InsertComma(_indexInsert);
            _indexInsert += 2;
        }
        else _isComma = true;

        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        _sb.Insert(_indexInsert, member.Name);
        _indexInsert += member.Name.Length;

        if (value is null)
            AppendNull();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    IInsertTranslator<T> IInsertTranslator<T>.Values<TField>(Expression<Func<T, TField>> field, TField value) 
        => Values(field, value);

    public static implicit operator InsertTranslator<T>(StringBuilder sb)
        => new InsertTranslator<T>(sb);
}
