using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public class InsertTranslator<T> : Translator
{
    private readonly List<string> _columns = new List<string>();
    private readonly List<object> _values = new List<object>();
    public override void Run(QueryBuilderOptions options)
    {
        options.StringBuilder.Append(" (");
        for (int i = 0; i < _columns.Count; i++)
        {
            if(i != 0 && i < _columns.Count) options.StringBuilder.Append(",");
            options.StringBuilder.Append(_columns[i]);
        }
        options.StringBuilder.Append(")");

        options.StringBuilder.Append("\r\nvalues (");
        for (int i = 0; i < _values.Count; i++)
        {
            if (i != 0 && i < _columns.Count) options.StringBuilder.Append(",");
            var columnParameterName = GetColumnParameterName(_columns[i], options.Parameters.Count());
            options.Parameters.Add(columnParameterName, _values[i]);
            options.StringBuilder.AppendFormat("@{0}", columnParameterName);
        }
        options.StringBuilder.Append(")");
    }

    public void AddValue<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        _columns.Add(CommonExpression.GetColumnName(column));
        _values.Add(value);
    }
}
