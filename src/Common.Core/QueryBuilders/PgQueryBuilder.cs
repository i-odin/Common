using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class PgQueryBuilder : QueryBuilder
{
    public override InsertQueryBuilder<T> Insert<T>(Action<TableTranslator<T>> inner)
    {
        var result = PgInsertQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }

    public override UpdateQueryBuilder<T> Update<T>(Action<TableTranslator<T>> inner)
    {
        var result = MsUpdateQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }

    public override DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner)
    {
        var result = PgDeleteQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }
}
