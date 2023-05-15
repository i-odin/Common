using Common.Core.QueryBuilders.Query;

namespace Common.Core.QueryBuilders.Translator;

public abstract class TranslatorNew
{
    public abstract void Run(QueryBuilderOptions options);
    protected string GetColumnParameterName(string fieldName, int index) 
        => string.Format("{0}{1}", fieldName, index);
}

public abstract class CommandTranslator : TranslatorNew
{
    protected readonly string _command;
    public CommandTranslator(string command) { _command = command; }
    public override void Run(QueryBuilderOptions options)
    {
        options.StringBuilder.AppendFormat("\r\n{0} ", _command);
    }
}

public abstract class AliasTranslator : CommandTranslator
{
    protected string _alias;
    public AliasTranslator(string command) : base(command) { }

    public virtual AliasTranslator WithAlias(string alias)
    {
        _alias = alias;
        return this;
    }
}

public abstract class TableTranslator<T> : AliasTranslator
{
    private string _table;
    private string _schema;
    public TableTranslator(string command, string schema) : base(command) { _schema = schema; }

    public override void Run(QueryBuilderOptions options)
    {
        var table = string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table;
        if (string.IsNullOrEmpty(_alias) == false)
            options.StringBuilder.AppendFormat("\r\n{0} {1}.{2} as {3}", _command, _schema, table, _alias);
        else
            options.StringBuilder.AppendFormat("\r\n{0} {1}.{2}", _command, _schema, table);
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