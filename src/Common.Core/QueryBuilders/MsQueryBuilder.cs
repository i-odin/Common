using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class MsQueryBuilder : QueryBuilder
{
    public override DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner)
    {
        var result = MsDeleteQueryBuilder<T>.Make(inner);
        Add(result);
        return result;
    }


    /*public IInsertQueryBuilder<T> Insert<T>(Action<IInsertTranslator<T>> inner)
        where T : class
        => InsertQueryBuilder<T>.Insert(_sb, inner);

    public IUpdateQueryBuilder<T> Update<T>(Action<IUpdateTranslator<T>> inner)
        where T : class
        => UpdateQueryBuilder<T>.Update(_sb, inner);
    */
}
