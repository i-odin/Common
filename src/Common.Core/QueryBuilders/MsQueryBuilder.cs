using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class MsQueryBuilder : QueryBuilder
{
    public override InsertQueryBuilder<T> Insert<T>(Action<TableTranslator<T>> inner)
    {
        var result = MsInsertQueryBuilder<T>.Make(inner);
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
        var result = MsDeleteQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }
}
