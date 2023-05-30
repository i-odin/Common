using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public class InsertTranslator<T> : Translator
{
    private readonly QueryBuilderSource _sourceColumns = new QueryBuilderSource();
    private readonly QueryBuilderSource _sourceValues = new QueryBuilderSource();

    public override void Run(QueryBuilderSource options)
    {
        options.Query.Append(" (");
        options.Query.Append(_sourceColumns.Query);
        foreach (var item in _sourceColumns.Parameters)
            options.Parameters.Add(new Parameter(item.Key, item.Value));
        options.Query.Remove(options.Query.Length - 1, 1);
        options.Query.Append(")");

        options.Query.Append("\r\nvalues (");
        options.Query.Append(_sourceValues.Query);
        foreach (var item in _sourceValues.Parameters)
            options.Parameters.Add(new Parameter(item.Key, item.Value));
        options.Query.Remove(options.Query.Length - 1, 1);
        options.Query.Append(")");
    }

    public InsertTranslator<T> Value<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new ColumnTranslator<T>().Value(column).Run(_sourceColumns);
        new ValueTranslator<T>().Value(column, value).Run(_sourceValues);
        return this;
    }

    public InsertTranslator<T> Value(string column, object value)
    {
        new ColumnTranslator<T>().Value(column).Run(_sourceColumns);
        new ValueTranslator<T>().Value(column, value).Run(_sourceValues);
        return this;
    }

    public static InsertTranslator<T> Make(Action<InsertTranslator<T>> inner)
    {
        var obj = new InsertTranslator<T>();
        inner?.Invoke(obj);
        return obj;
    }
}

public class ColumnTranslator<T> : Translator
{
    protected string _column;
    
    public override void Run(QueryBuilderSource options)
    {
        options.Query.Append(_column);
        options.Query.Append(",");
    }

    public ColumnTranslator<T> Value<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        _column = CommonExpression.GetColumnName(column);
        return this;
    }

    public ColumnTranslator<T> Value(string column)
    {
        _column = column;
        return this;
    }
}

public class ValueTranslator<T> : ColumnTranslator<T>
{
    private object _value;
    public override void Run(QueryBuilderSource options)
    {
        var columnParameterName = GetColumnParameterName(_column, options.Parameters.Count());
        options.Parameters.Add(columnParameterName, _value);
        options.Query.Append("@").Append(columnParameterName);
        options.Query.Append(",");
    }

    public ValueTranslator<T> Value<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _value = value;
        Value(column);
        return this;
    }

    public ValueTranslator<T> Value(string column, object value)
    {
        _value = value;
        Value(column);
        return this;
    }
}