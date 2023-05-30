using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class DeleteQueryBuilder<T> : QueryBuilder
{
    protected DeleteQueryBuilder(QueryBuilderSource source) : base(source) {}
    public abstract DeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner);
    public DeleteQueryBuilder<T> Delete() => Delete(inner: null);
    public DeleteQueryBuilder<T> Where(Action<WhereTranslator<T>> inner)
    {
        MsWhereTranslator<T>.Make(inner).Run(_source);
        return this;
    }
}

public class MsDeleteQueryBuilder<T> : DeleteQueryBuilder<T> 
{
    private readonly string _command = "delete";
    public MsDeleteQueryBuilder(QueryBuilderSource source) : base(source) { }
    public override MsDeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner)
    {
        MsTableTranslator<T>.Make(_command, inner).Run(_source);
        return this;
    }

    public static MsDeleteQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new MsDeleteQueryBuilder<T>(source).Delete(inner);
}

public class PgDeleteQueryBuilder<T> : DeleteQueryBuilder<T>
{
    private readonly string _command = "delete from";

    public PgDeleteQueryBuilder(QueryBuilderSource source) : base(source) { }

    public override PgDeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner)
    {
        PgTableTranslator<T>.Make(_command, inner).Run(_source);
        return this;
    }

    public static PgDeleteQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new PgDeleteQueryBuilder<T>(source).Delete(inner);
}