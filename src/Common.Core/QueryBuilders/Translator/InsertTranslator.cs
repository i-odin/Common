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
    public InsertTranslator(StringBuilder sb) : base(sb) { }
    public Translator<T> Insert()
    {
        AppendNewLine("insert into ").Append(typeEntity.Name).Append(" ").BracketLeft();
        _indexInsert = Length;

        AppendNewLine("values ").BracketLeft();
        return this;
    }

    public Translator<T> InsertEnd()
    {
        Insert(_indexInsert, BracketRitht());
        return this;
    }

    public InsertTranslator<T> Values<TField>(Expression<Func<T, TField>> field, TField value)
    {
        if (_isComma)
        {
            Insert(_indexInsert, Comma());
            _indexInsert += 2;
        }
        else _isComma = true;

        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        Insert(_indexInsert, member.Name);
        _indexInsert += member.Name.Length;

        Value(value);
        return this;
    }

    public static InsertTranslator<T> Insert(StringBuilder sb, Action<InsertTranslator<T>> inner)
    {
        var obj = new InsertTranslator<T>(sb);
        obj.Insert();
        inner(obj);
        obj.InsertEnd();
        return obj;
    }

    public static implicit operator InsertTranslator<T>(StringBuilder sb)
        => new InsertTranslator<T>(sb);

    IInsertTranslator<T> IInsertTranslator<T>.Values<TField>(Expression<Func<T, TField>> field, TField value)
        => Values(field, value);
}
