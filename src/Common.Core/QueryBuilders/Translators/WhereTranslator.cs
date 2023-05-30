using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public abstract class WhereTranslator<T> : CommandTranslator
{
    private readonly QueryBuilderSource _source = new QueryBuilderSource();

    public WhereTranslator(string command) : base(command) { }
    public override void Run(QueryBuilderSource options)
    {
        base.Run(options);
        options.Query.Append(_source.Query);
    }

    public WhereTranslator<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new EqualToTranslator<T>(CommonExpression.GetColumnName(column), value).Run(_source);
        return this;
    }

    public WhereTranslator<T> EqualTo(string column, object value)
    {
        new EqualToTranslator<T>(column, value).Run(_source);
        return this;
    }

    public WhereTranslator<T> And()
    {
        new AndTranslator().Run(_source);
        return this;
    }
}

public class MsWhereTranslator<T> : WhereTranslator<T>
{
    public MsWhereTranslator(string command = "where") : base(command) { }
    public static MsWhereTranslator<T> Make(Action<WhereTranslator<T>> inner)
    {
        var obj = new MsWhereTranslator<T>();
        inner?.Invoke(obj);
        return obj;
    }
}

public class EqualToTranslator<T> : Translator
{
    private string _columnName;
    private object _value;
    
    public EqualToTranslator(string columnName, object value)
    {
        _value = value;
        _columnName = columnName;
    }

    public override void Run(QueryBuilderSource source)
    {
        var columnParameterName = GetColumnParameterName(_columnName, source.Parameters.Count());
        source.Parameters.Add(columnParameterName, _value);
        source.Query.Append(_columnName).Append(" = @").Append(columnParameterName);
    }
}

public class AndTranslator : Translator
{
    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append(" and ");
    }
}