using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class DeleteQueryBuilder<T> : QueryBuilder
{
    public abstract DeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner);
    public DeleteQueryBuilder<T> Delete() => Delete(inner: null);
    public DeleteQueryBuilder<T> Where(Action<WhereTranslator<T>> inner)
    {
        Add(MsWhereTranslator<T>.Make(inner));
        return this;
    }
}

public class MsDeleteQueryBuilder<T> : DeleteQueryBuilder<T> 
{
    private readonly string _command = "delete";
    public override MsDeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner)
    {
        Add(MsTableTranslator<T>.Make(_command, inner));
        return this;
    }

    public static MsDeleteQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new MsDeleteQueryBuilder<T>().Delete(inner);
}

public class PgDeleteQueryBuilder<T> : DeleteQueryBuilder<T>
{
    private readonly string _command = "delete from";
    public override PgDeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner)
    {
        Add(PgTableTranslator<T>.Make(_command, inner));
        return this;
    }

    public static PgDeleteQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new PgDeleteQueryBuilder<T>().Delete(inner);
}