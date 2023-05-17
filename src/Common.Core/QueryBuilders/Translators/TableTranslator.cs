using Common.Core.QueryBuilders.Queris;

namespace Common.Core.QueryBuilders.Translators;

public abstract class TableTranslator<T> : CommandTranslator
{
    private string _alias;
    private string _table;
    private string _schema;
    public TableTranslator(string command, string schema) : base(command) { _schema = schema; }

    public override void Run(QueryBuilderOptions options)
    {
        var table = string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table;
        options.StringBuilder.Append("\r\n");
        if (string.IsNullOrEmpty(_alias) == false)
        {
            options.StringBuilder.Append(_command).Append(" ").Append(_schema).Append(".").Append(table).Append(" as ").Append(_alias);
        }
        else
        {
            options.StringBuilder.Append(_command).Append(" ").Append(_schema).Append(".").Append(table);
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
    public MsTableTranslator( string command, string schema = "dbo") : base(command, schema) { }

    public static TableTranslator<T> Make(string command, Action<TableTranslator<T>> inner)
    {
        var obj = new MsTableTranslator<T>(command);
        inner?.Invoke(obj);
        return obj;
    }
}

public class PgTableTranslator<T> : TableTranslator<T>
{
    public PgTableTranslator(string command, string schema = "public") : base(command, schema) { }

    public static TableTranslator<T> Make(string command, Action<TableTranslator<T>> inner)
    {
        var obj = new PgTableTranslator<T>(command);
        inner?.Invoke(obj);
        return obj;
    }
}