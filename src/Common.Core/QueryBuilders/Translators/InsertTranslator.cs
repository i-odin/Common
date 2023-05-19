using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public class InsertTranslator<T> : Translator
{
    private readonly List<string> _columns = new List<string>();
    private readonly List<object> _values = new List<object>();
    public override void Run(QueryBuilderSource options)
    {
        options.Query.Append(" (");
        for (int i = 0; i < _columns.Count; i++)
        {
            if(i != 0 && i < _columns.Count) options.Query.Append(",");
            options.Query.Append(_columns[i]);
        }
        options.Query.Append(")");

        options.Query.Append("\r\nvalues (");
        for (int i = 0; i < _values.Count; i++)
        {
            if (i != 0 && i < _columns.Count) options.Query.Append(",");
            var columnParameterName = GetColumnParameterName(_columns[i], options.Parameters.Count());
            options.Parameters.Add(columnParameterName, _values[i]);
            options.Query.Append("@").Append(columnParameterName);
        }
        options.Query.Append(")");
    }

    public void AddValue<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _columns.Add(CommonExpression.GetColumnName(column));
        _values.Add(value);
    }

    public void AddValue(string column, object value)
    {
        _columns.Add(column);
        _values.Add(value);
    }
}
