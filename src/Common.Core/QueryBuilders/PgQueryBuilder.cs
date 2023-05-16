using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class PgQueryBuilder : QueryBuilder
{
    public override DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner)
    {
        var result = PgDeleteQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }
}
