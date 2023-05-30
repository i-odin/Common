using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders;

public class PgCommonQueryBuilder : CommonQueryBuilder
{
    public override InsertQueryBuilder<T> Insert<T>(Action<TableTranslator<T>> inner)
        => PgInsertQueryBuilder<T>.Make(source, inner);

    public override UpdateQueryBuilder<T> Update<T>(Action<TableTranslator<T>> inner)
        => MsUpdateQueryBuilder<T>.Make(source, inner);

    public override DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner)
        => PgDeleteQueryBuilder<T>.Make(source, inner);
}
