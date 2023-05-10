using Common.Core.QueryBuilders.Query;

namespace Common.Core.QueryBuilders.Translator;

public abstract class TranslatorNew
{
    public abstract void Run(QueryBuilderOptions options);
    protected string GetColumnParameterName(string fieldName, int index) 
        => string.Format("{0}{1}", fieldName, index);
}

public abstract class ShortTableTranslator<T> : TranslatorNew
{
    private string _table;
    protected string _command;
    protected string _schema;
    protected string table { get { return string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table; } }
    public ShortTableTranslator(string command, string schema) { _command = command; _schema = schema; }

    public override void Run(QueryBuilderOptions options)
    {
        options.StringBuilder.AppendFormat("\r\n{0} {1}.{2}", _command, _schema, table);
    }

    public virtual ShortTableTranslator<T> WithTable(string table)
    {
        _table = table;
        return this;
    }

    public virtual ShortTableTranslator<T> WithSchema(string schema)
    {
        _schema = schema;
        return this;
    }
}

public abstract class TableTranslator<T> : ShortTableTranslator<T>
{
    private string _alias;
    public TableTranslator(string command, string schema) : base(command, schema) {}

    public override void Run(QueryBuilderOptions options)
    {
        if (string.IsNullOrEmpty(_alias) == false)
            options.StringBuilder.AppendFormat("\r\n{0} {1}.{2} as {3}", _command, _schema, table, _alias);
        else
            base.Run(options);
    }

    public override TableTranslator<T> WithTable(string table)
    {
        base.WithTable(table);
        return this;
    }

    public override TableTranslator<T> WithSchema(string schema)
    {
        base.WithSchema(schema);
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
    public MsTableTranslator(string command, string schema = "dbo") : base(command, schema) { }

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