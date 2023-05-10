using Common.Core.QueryBuilders.Translator;

namespace Common.Core.QueryBuilders.Query;

public abstract class DeleteQueryBuilder<T> : RootQueryBuilder
{
    public abstract DeleteQueryBuilder<T> Delete(Action<TranslatorShortTable<T>> inner);
    public DeleteQueryBuilder<T> Delete(string table) => Delete(x => x.WithTable(table));
    public DeleteQueryBuilder<T> Delete() => Delete(inner: null);
}

public class MsDeleteQueryBuilder<T> : DeleteQueryBuilder<T> 
{
    private readonly string _command = "delete";
    public override MsDeleteQueryBuilder<T> Delete(Action<TranslatorShortTable<T>> inner)
    {
        Add(MsTranslatorTable<T>.Make(_command, inner));
        return this;
    }

    public static MsDeleteQueryBuilder<T> Make(Action<TranslatorShortTable<T>> inner)
        => new MsDeleteQueryBuilder<T>().Delete(inner);
}

public class PgDeleteQueryBuilder<T> : DeleteQueryBuilder<T>
{
    private readonly string _command = "delete from";
    public override PgDeleteQueryBuilder<T> Delete(Action<TranslatorShortTable<T>> inner)
    {
        Add(PgTranslatorTable<T>.Make(_command, inner));
        return this;
    }

    public static PgDeleteQueryBuilder<T> Make(Action<TranslatorShortTable<T>> inner)
        => new PgDeleteQueryBuilder<T>().Delete(inner);
}