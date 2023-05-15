using Common.Core.QueryBuilders.Query;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translator;

public abstract class WhereTranslator<T> : AliasTranslator
{
    private readonly ICollection<TranslatorNew> _translators = new List<TranslatorNew>();

    public WhereTranslator(string command) : base(command) { }
    public override void Run(QueryBuilderOptions options)
    {
        base.Run(options);
        
        foreach (var item in _translators)
            item.Run(options);
    }

    public WhereTranslator<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _translators.Add(new EqualToTranslator<T>(CommonExpression.GetColumnName(column), value));
        return this;
    }
    public WhereTranslator<T> And()
    {
        _translators.Add(new AndTranslator());
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

public class AndTranslator : TranslatorNew
{
    public override void Run(QueryBuilderOptions options)
    {
        options.StringBuilder.Append(" and ");
    }
}