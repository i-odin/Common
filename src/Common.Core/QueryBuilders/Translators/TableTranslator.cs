using Common.Core.QueryBuilders.Queris;

namespace Common.Core.QueryBuilders.Translators;

public abstract class TableTranslator<T> : CommandTranslator
{
    private string _alias;
    private string _table;
    private string _schema;
    public TableTranslator(string command, string schema, QueryBuilderSource source) : base(command, source) { _schema = schema; }

    public override void Run()
    {
        var table = string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table;
        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_alias) == false)
        {
            source.Query.Append(_command).Append(" ").Append(_schema).Append(".").Append(table).Append(" as ").Append(_alias);
        }
        else
        {
            source.Query.Append(_command).Append(" ").Append(_schema).Append(".").Append(table);
        }
    }

    public TableTranslator<T> WithTable(string table)
    {
        _table = table;
        return this;
    }

    public TableTranslator<T> WithSchema(string schema)
    {
        _schema = schema;
        return this;
    }
    public TableTranslator<T> WithAlias(string alias)
    {
        _alias = alias;
        return this;
    }
}

public class MsTableTranslator<T> : TableTranslator<T>
{
    public MsTableTranslator( string command, string schema, QueryBuilderSource source) : base(command, schema, source) { }

    public static TableTranslator<T> Make(string command, QueryBuilderSource source, Action<TableTranslator<T>> inner)
    {
        var obj = new MsTableTranslator<T>(command, "dbo", source);
        inner?.Invoke(obj);
        return obj;
    }
}

public class PgTableTranslator<T> : TableTranslator<T>
{
    public PgTableTranslator(string command, string schema, QueryBuilderSource source) : base(command, schema, source) { }

    public static TableTranslator<T> Make(string command, QueryBuilderSource source, Action<TableTranslator<T>> inner)
    {
        var obj = new PgTableTranslator<T>(command, "public", source);
        inner?.Invoke(obj);
        return obj;
    }
}