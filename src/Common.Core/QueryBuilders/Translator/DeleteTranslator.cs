using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public abstract class TranslatorNew
{
    private ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    public virtual void Run(StringBuilder sb)
    {
        foreach (var translator in _translators)
            translator.Run(sb);
    }

    protected void Add(TranslatorNew translator)
        => _translators.Add(translator);
}

public abstract class TranslatorTable<T> : TranslatorNew
{
    private string _command;
    private string _table;
    private string _schema;
    private string _alias;
    public TranslatorTable(string command, string schema) { _command = command; _schema = schema; }

    public override void Run(StringBuilder sb)
    {
        var tableName = string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table;
        if (string.IsNullOrEmpty(_alias) == false)
            sb.AppendFormat("\r\n{0} {1}.{2} as {3}", _command, _schema, tableName, _alias);
        else
            sb.AppendFormat("\r\n{0} {1}.{2}", _command, _schema, tableName);
    }

    public TranslatorTable<T> WithTable(string table)
    {
        _table = table;
        return this;
    }

    public TranslatorTable<T> WithSchema(string schema)
    {
        _schema = schema;
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
    public MsTranslatorTable(string command, string schema) : base(command, schema) { }

    public static TranslatorTable<T> Make(string command, Action<TranslatorTable<T>> inner)
    {
        var obj = new MsTranslatorTable<T>(command, "dbo");
        inner?.Invoke(obj);
        return obj;
    }
}

public class PgTranslatorTable<T> : TranslatorTable<T>
{
    public PgTranslatorTable(string command, string schema) : base(command, schema) { }

    public static TranslatorTable<T> Make(string command, Action<TranslatorTable<T>> inner)
    {
        var obj = new PgTranslatorTable<T>(command, "public");
        inner?.Invoke(obj);
        return obj;
    }
}

public abstract class DeleteTranslator<T> : TranslatorNew
{
    public abstract DeleteTranslator<T> Delete(Action<TranslatorTable<T>> inner);
}

public class MsDeleteTranslator<T> : DeleteTranslator<T>
{
    public override MsDeleteTranslator<T> Delete(Action<TranslatorTable<T>> inner)
    {
        Add(MsTranslatorTable<T>.Make("delete from", inner));
        return this;
    }
    public static DeleteTranslator<T> Make(Action<TranslatorTable<T>> inner)
        => new MsDeleteTranslator<T>().Delete(inner);
}

public class PgDeleteTranslator<T> : DeleteTranslator<T>
{
    public override PgDeleteTranslator<T> Delete(Action<TranslatorTable<T>> inner)
    {
        Add(PgTranslatorTable<T>.Make("delete from", inner));
        return this;
    }

    public static DeleteTranslator<T> Make(Action<TranslatorTable<T>> inner)
        => new PgDeleteTranslator<T>().Delete(inner);
}

/*
public interface IDeleteTranslator<T>
{ }

public class DeleteTranslator<T> : Translator<T>, IDeleteTranslator<T>
{
    public DeleteTranslator(StringBuilder sb) : base(sb) { }

    public DeleteTranslator<T> Delete()
    {
        AppendNewLine("delete ").Append(typeEntity.Name);
        return this;
    }

    public static DeleteTranslator<T> Delete(StringBuilder sb) 
        => new DeleteTranslator<T>(sb).Delete();

    public static implicit operator DeleteTranslator<T>(StringBuilder sb)
        => new DeleteTranslator<T>(sb);
}
*/