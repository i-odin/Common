using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translators;

/*public interface IJoinTranslator<TJoin1, TJoin2>
{
    IJoinTranslator<TJoin1, TJoin2> Equal<TField>([NotNull] Expression<Func<TJoin1, TField>> field1, [NotNull] Expression<Func<TJoin2, TField>> field2);
}

public class JoinTranslator<TJoin1, TJoin2> : Translator<TJoin1>, IJoinTranslator<TJoin1, TJoin2>
{
    private Type _typeJoin2;
    protected Type typeJoin2
    {
        get
        {
            if (_typeJoin2 == null) _typeJoin2 = typeof(TJoin2);
            return _typeJoin2;
        }
    }

    public JoinTranslator(StringBuilder sb) : base(sb) { }

    public JoinTranslator<TJoin1, TJoin2> Field<TField>(Expression<Func<TJoin2, TField>> field)
    {
        //TODO: убрать дубликат
        var member = (field.Body as MemberExpression)?.Member;
        if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

        Append(member.Name);
        return this;
    }

    public JoinTranslator<TJoin1, TJoin2> Equal<TField>([NotNull] Expression<Func<TJoin1, TField>> field1, [NotNull] Expression<Func<TJoin2, TField>> field2)
    {
        if (_isComma) Comma();
        else _isComma = true;

        Append(typeEntity.Name).Point();
        Field(field1);
        Equal();
        Append(typeJoin2.Name).Point();
        Field(field2);
        return this;
    }

    //INNER JOIN Geeks2 ON Geeks1.col1 = Geeks2.col1
    public JoinTranslator<TJoin1, TJoin2> Join()
    {
        AppendNewLine("join ").Append(typeJoin2.Name).Append(" on ");
        return this;
    }

    public JoinTranslator<TJoin1, TJoin2> UpdateJoin()
    {
        AppendNewLine("from ").Append(typeEntity.Name);
        Join();
        return this;
    }

    public static JoinTranslator<TJoin1, TJoin2> Join(StringBuilder sb, Action<JoinTranslator<TJoin1, TJoin2>> inner)
    {
        var obj = new JoinTranslator<TJoin1, TJoin2>(sb).Join();
        inner(obj);
        return obj;
    }

    public static JoinTranslator<TJoin1, TJoin2> UpdateJoin(StringBuilder sb, Action<JoinTranslator<TJoin1, TJoin2>> inner)
    {
        var obj = new JoinTranslator<TJoin1, TJoin2>(sb).UpdateJoin();
        inner(obj);
        return obj;
    }

    public static implicit operator JoinTranslator<TJoin1, TJoin2>(StringBuilder sb)
        => new JoinTranslator<TJoin1, TJoin2>(sb);

    IJoinTranslator<TJoin1, TJoin2> IJoinTranslator<TJoin1, TJoin2>.Equal<TField>(Expression<Func<TJoin1, TField>> field1, Expression<Func<TJoin2, TField>> field2)
    {
        Equal(field1, field2);
        return this;
    }
}*/
