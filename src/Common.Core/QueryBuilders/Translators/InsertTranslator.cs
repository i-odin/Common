using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public class InsertTranslator<T> : Translator
{
    private bool _isOpen;
    private int _indexColumn;
    private int _indexValue;
    public InsertTranslator(QueryBuilderSource source) : base(source) { }

    public override void Run()
    {
        source.Query.Remove(_indexColumn - 1, 1);
        source.Query.Insert(_indexColumn - 1, ")");

        source.Query.Remove(_indexValue - 1, 1);
        source.Query.Append(")");
    }

    public InsertTranslator<T> Value<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        Value(CommonExpression.GetColumnName(column), value);
        return this;
    }

    public InsertTranslator<T> Value(string column, object value)
    {
        if (_isOpen == false)
        {
            source.Query.Append(" (");
            _indexColumn = source.Query.Length;

            source.Query.Append("\r\nvalues (");
            _indexValue = source.Query.Length;
            _isOpen = true;
        }

        source.Query.Insert(_indexColumn, column);
        
        _indexColumn += column.Length;
        _indexValue += column.Length;

        source.Query.Insert(_indexColumn, ",");
        
        _indexColumn++;
        _indexValue++;

        source.Parameters.Add(value, out string name);
        
        source.Query.Insert(_indexValue, "@");
        _indexValue++;
        
        source.Query.Insert(_indexValue, name);
        _indexValue += name.Length;
        
        source.Query.Insert(_indexValue, ",");
        _indexValue++;

        return this;
    }

    public static InsertTranslator<T> Make(QueryBuilderSource source, Action<InsertTranslator<T>> inner)
    {
        var obj = new InsertTranslator<T>(source);
        inner?.Invoke(obj);
        return obj;
    }
}