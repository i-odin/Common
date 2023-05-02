using Common.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public class Translator
{
    protected readonly StringBuilder _sb;
    public Translator(StringBuilder sb) => _sb = sb;

    public int Length => _sb.Length;

    public Translator Append(string value)
    {
        _sb.Append(value);
        return this;
    }

    public Translator AppendNewLine(string value)
    {
        if (_sb.Length > 0)
            Append("\r\n");
        Append(value);
        return this;
    }

    public Translator Insert(int index, string value)
    {
        _sb.Insert(index, value);
        return this;
    }

    public Translator AppendString(string value)
    {
        Append("'");
        Append(value);
        Append("'");
        return this;
    }

    public Translator Null()
    {
        Append("null");
        return this;
    }

    public Translator BracketLeft()
    {
        Append("(");
        return this;
    }

    public string BracketRitht()
    {
        var result = ")";
        Append(result);
        return result;
    }

    public Translator Or()
    {
        Append(" or ");
        return this;
    }

    public Translator And()
    {
        Append(" and ");
        return this;
    }

    public Translator NotEqual()
    {
        Append(" <> ");
        return this;
    }

    public Translator Equal()
    {
        Append(" = ");
        return this;
    }

    public string Comma()
    {
        var result = ", ";
        Append(result);
        return result;
    }
    
    public override string ToString() => _sb.ToString();
}

public class Translator<T> : Translator
     where T : class
{
    private int _indexInsert;
    private bool _isComma;
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
            Null();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> Equal<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
    {
        Field(field).Equal();
        if (value is null)
            Null();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> Field<TField>(Expression<Func<T, TField>> field)
    {
        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        Append(member.Name);
        return this;
    }

    public Translator<T> Value(string value)
    {
        if (value != null) AppendString(value);
        else Null();
        return this;
    }

    public Translator<T> Value(Guid value)
    {
        if (value != null) AppendString(value.ToString());
        else Null();
        return this;
    }

    public Translator<T> Value(DateTime? value)
    {
        if (value.HasValue) Value(value.Value);
        else Null();
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
        else Null();
        return this;
    }

    public Translator<T> Value(int value)
    {
        Append(value.ToString());
        return this;
    }

    public Translator<T> Delete()
    {
        AppendNewLine("delete ").Append(typeEntity.Name);
        return this;
    }

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

    public Translator<T> Values<TField>(Expression<Func<T, TField>> field, TField value)
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

        if (value is null)
            Null();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> Join()
    {
        /*
         FROM Geeks1  
INNER JOIN Geeks2 ON Geeks1.col1 = Geeks2.col1  

         */
        /*AppendNewLine("from ");
        _sb.Append(typeEntity.Name);
        _sb.Append(" ");
        BracketLeft();
        _indexInsert = _sb.Length;

        AppendNewLine("values ");
        BracketLeft();*/
        return this;
    }

    public Translator<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
    {
        if (_isComma) Comma();
        else _isComma = true;

        Field(field).Equal();
        if (value is null)
            Null();
        else //dynemic оказался быстрее, чем каст к типу (value is string)
            Value((dynamic)value);

        return this;
    }

    public Translator<T> Update()
    {
        AppendNewLine("update ").Append(typeEntity.Name).AppendNewLine("set ");
        return this;
    }
    

    public Translator<T> Where()
    {
        AppendNewLine("where ");
        return this;
    }
}
