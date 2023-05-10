using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;

namespace Common.Core.QueryBuilders;

public class PgQueryBuilder : QueryBuilder
{
    public override DeleteQueryBuilder<T> Delete<T>(Action<TranslatorShortTable<T>> inner)
    {
        var result = PgDeleteQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }
}
