using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public abstract class TranslatorNew
{
    public abstract void Run(StringBuilder sb);
}

public abstract class TranslatorShortTable<T> : TranslatorNew
{
    private string _table;
    protected string _command;
    protected string _schema;
    protected string table { get { return string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table; } }
    public TranslatorShortTable(string command, string schema) { _command = command; _schema = schema; }

    public override void Run(StringBuilder sb)
    {
        sb.AppendFormat("\r\n{0} {1}.{2}", _command, _schema, table);
    }

    public virtual TranslatorShortTable<T> WithTable(string table)
    {
        _table = table;
        return this;
    }

    public virtual TranslatorShortTable<T> WithSchema(string schema)
    {
        _schema = schema;
        return this;
    }
}

public abstract class TranslatorTable<T> : TranslatorShortTable<T>
{
    private string _alias;
    public TranslatorTable(string command, string schema) : base(command, schema) {}

    public override void Run(StringBuilder sb)
    {
        if (string.IsNullOrEmpty(_alias) == false)
            sb.AppendFormat("\r\n{0} {1}.{2} as {3}", _command, _schema, table, _alias);
        else
            base.Run(sb);
    }

    public override TranslatorTable<T> WithTable(string table)
    {
        base.WithTable(table);
        return this;
    }

    public override TranslatorTable<T> WithSchema(string schema)
    {
        base.WithSchema(schema);
        return this;
    }

    public TranslatorTable<T> WithAlias(string alias)
    {
        _alias = alias;
        return this;
    }
}

public class MsTranslatorTable<T> : TranslatorTable<T>
{
    public MsTranslatorTable(string command, string schema = "dbo") : base(command, schema) { }

    public static TranslatorTable<T> Make(string command, Action<TranslatorTable<T>> inner)
    {
        var obj = new MsTranslatorTable<T>(command);
        inner?.Invoke(obj);
        return obj;
    }
}

public class PgTranslatorTable<T> : TranslatorTable<T>
{
    public PgTranslatorTable(string command, string schema = "public") : base(command, schema) { }

    public static TranslatorTable<T> Make(string command, Action<TranslatorTable<T>> inner)
    {
        var obj = new PgTranslatorTable<T>(command);
        inner?.Invoke(obj);
        return obj;
    }
}