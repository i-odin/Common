using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public class InsertTranslator<T> : Translator
{
    private readonly TranslatorManager _managerColumns = new TranslatorManager();
    private readonly TranslatorManager _managerValues = new TranslatorManager();

    public override void Run(QueryBuilderSource options)
    {
        options.Query.Append(" (");
        _managerColumns.Run(options);
        options.Query.Remove(options.Query.Length - 1, 1);
        options.Query.Append(")");

        options.Query.Append("\r\nvalues (");
        _managerValues.Run(options);
        options.Query.Remove(options.Query.Length - 1, 1);
        options.Query.Append(")");
    }

    public InsertTranslator<T> Value<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _managerColumns.Add(new ColumnTranslator<T>().Value(column));
        _managerValues.Add(new ValueTranslator<T>().Value(column, value));
        return this;
    }

    public InsertTranslator<T> Value(string column, object value)
    {
        _managerColumns.Add(new ColumnTranslator<T>().Value(column));
        _managerValues.Add(new ValueTranslator<T>().Value(column, value));
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