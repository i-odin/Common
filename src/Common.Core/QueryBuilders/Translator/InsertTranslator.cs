using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IInsertTranslator<T>
    where T : class
{
    IInsertTranslator<T> Field<TField>(Expression<Func<T, TField>> field);
}

public class InsertTranslator<T> : Translator<T>, IInsertTranslator<T>
    where T : class
{
    private bool _isComma;
    public InsertTranslator(StringBuilder sb) : base(sb) { }

    IInsertTranslator<T> IInsertTranslator<T>.Field<TField>(Expression<Func<T, TField>> field)
    {
        if (_isComma) Comma();
        else _isComma = true;

        Field(field);
        return this;
    }

    private InsertTranslator<T> Insert()
    {
        AppendNewLine("insert ");
        Append(typeEntity.Name);
        Append(" ");
        return this;
    }

    public static implicit operator InsertTranslator<T>(StringBuilder sb)
        => new InsertTranslator<T>(sb).Insert();
}
