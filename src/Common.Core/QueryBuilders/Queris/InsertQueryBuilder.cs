using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class InsertQueryBuilder<T> : QueryBuilder
{
    protected InsertQueryBuilder(QueryBuilderSource _source) : base(_source) {}
    public abstract InsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner);
    public InsertQueryBuilder<T> Insert() => Insert(inner: null);
    public InsertQueryBuilder<T> Values(Action<InsertTranslator<T>> inner)
    {
        InsertTranslator<T>.Make(_source, inner).Run();
        return this;
    }
}

public class MsInsertQueryBuilder<T> : InsertQueryBuilder<T>
{
    private readonly string _command = "insert into";

    public MsInsertQueryBuilder(QueryBuilderSource _source) : base(_source) {}

    public override MsInsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner)
    {
        MsTableTranslator<T>.Make(_command, _source, inner).Run();
        return this;
    }

    public static MsInsertQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new MsInsertQueryBuilder<T>(source).Insert(inner);
}

public class PgInsertQueryBuilder<T> : InsertQueryBuilder<T>
{
    private readonly string _command = "insert into";

    public PgInsertQueryBuilder(QueryBuilderSource _source) : base(_source) {}

    public override PgInsertQueryBuilder<T> Insert(Action<TableTranslator<T>> inner)
    {
        PgTableTranslator<T>.Make(_command, _source, inner).Run();
        return this;
    }

    public static PgInsertQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new PgInsertQueryBuilder<T>(source).Insert(inner);
}