using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class MsCommonQueryBuilder : CommonQueryBuilder
{
    public override InsertQueryBuilder<T> Insert<T>(Action<TableTranslator<T>> inner) 
        => manager.Add(MsInsertQueryBuilder<T>.Make(inner));

    public override UpdateQueryBuilder<T> Update<T>(Action<TableTranslator<T>> inner) 
        => manager.Add(MsUpdateQueryBuilder<T>.Make(inner));

    public override DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner)
        => manager.Add(MsDeleteQueryBuilder<T>.Make(inner));
}
