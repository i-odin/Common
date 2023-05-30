using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public abstract class CommonQueryBuilder
{
    protected QueryBuilderSource source = new QueryBuilderSource();
    public abstract InsertQueryBuilder<T> Insert<T>(Action<TableTranslator<T>> inner);
    public InsertQueryBuilder<T> Insert<T>() => Insert<T>(null);
    public abstract UpdateQueryBuilder<T> Update<T>(Action<TableTranslator<T>> inner);
    public UpdateQueryBuilder<T> Update<T>() => Update<T>(null);
    public abstract DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner);
    public DeleteQueryBuilder<T> Delete<T>() => Delete<T>(null);

    public override string ToString()
        => source.Query.ToString();
}

public static class CommonQueryBuilderExtension
{
    public static void UseDapper(this CommonQueryBuilder builder)
    {
        throw new NotImplementedException();
    }
}
