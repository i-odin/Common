using Common.Core.QueryBuilders.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Queris;

public abstract class InsertQueryBuilder<T> : BaseQueryBuilder
{
    protected int _index;
    public abstract InsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner);
    public InsertQueryBuilder<T> Insert() => Insert(inner: null);
    public InsertQueryBuilder<T> Value<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        (Get(_index) as InsertTranslator<T>).AddValue(column, value);
        return this;
    }
    public InsertQueryBuilder<T> Value(string column, object value)
    {
        (Get(_index) as InsertTranslator<T>).AddValue(column, value);
        return this;
    }
}

public class MsInsertQueryBuilder<T> : InsertQueryBuilder<T>
{
    private readonly string _command = "insert into";
    public override MsInsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner)
    {
        Add(MsTableTranslator<T>.Make(_command, inner));
        _index = Add(new InsertTranslator<T>());
        return this;
    }

    public static MsInsertQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new MsInsertQueryBuilder<T>().Insert(inner);
}

public class PgInsertQueryBuilder<T> : InsertQueryBuilder<T>
{
    private readonly string _command = "insert into";
    public override PgInsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner)
    {
        Add(PgTableTranslator<T>.Make(_command, inner));
        _index = Add(new InsertTranslator<T>());
        return this;
    }

    public static PgInsertQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new PgInsertQueryBuilder<T>().Insert(inner);
}