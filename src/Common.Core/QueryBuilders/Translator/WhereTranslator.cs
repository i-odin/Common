using Common.Core.QueryBuilders.Query;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translator;

public abstract class WhereTranslator<T> : TranslatorNew
{
    private ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    public override void Run(QueryBuilderOptions options)
    {
        //добавить вере
        foreach (var item in _translators)
            item.Run(options);
    }

    public WhereTranslator<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _translators.Add(new EqualToTranslator<T>(CommonExpression.GetColumnName(column), value));
        return this;
    }
}

public class EqualToTranslator<T> : TranslatorNew
{
    private string _columnName;
    private object _value;
    
    public EqualToTranslator(string columnName, object value)
    {
        _value = value;
        _columnName = columnName;
    }

    public override void Run(QueryBuilderOptions options)
    {
        var columnParameterName = GetColumnParameterName(_columnName, options.Parameters.Count());
        options.Parameters.Add(columnParameterName, _value);
        options.StringBuilder.AppendFormat("{0} = @{1}", _columnName, columnParameterName);
    }
}

/*
public interface IWhereTranslator<T>
{
    IWhereTranslator<T> Equal<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
    IWhereTranslator<T> NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
    IWhereTranslator<T> Bracket(Action<IWhereTranslator<T>> inner);
    IWhereTranslator<T> And();
    IWhereTranslator<T> Or();
}

public class WhereTranslator<T> : Translator<T>, IWhereTranslator<T>
{
    public WhereTranslator(StringBuilder sb) : base(sb) { }

    public WhereTranslator<T> Bracket(Action<WhereTranslator<T>> inner)
    {
        BracketLeft();
        inner(_sb);
        BracketRitht();
        return this;
    }

    public WhereTranslator<T> Where()
    {
        AppendNewLine("where ");
        return this;
    }

    public static WhereTranslator<T> Where(StringBuilder sb, Action<WhereTranslator<T>> inner)
    {
        var obj = new WhereTranslator<T>(sb).Where();
        inner(obj);
        return obj;
    }

    public static implicit operator WhereTranslator<T>(StringBuilder sb)
        => new WhereTranslator<T>(sb);

    IWhereTranslator<T> IWhereTranslator<T>.NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
    {
        NotEqual(field, value);
        return this;
    }

    IWhereTranslator<T> IWhereTranslator<T>.Equal<TField>(Expression<Func<T, TField>> field, TField value)
    {
        Equal(field, value);
        return this;
    }

    IWhereTranslator<T> IWhereTranslator<T>.And()
    {
        And();
        return this;
    }

    IWhereTranslator<T> IWhereTranslator<T>.Or()
    {
        Or();
        return this;
    }

    IWhereTranslator<T> IWhereTranslator<T>.Bracket(Action<IWhereTranslator<T>> inner)
        => Bracket(inner);
}
*/