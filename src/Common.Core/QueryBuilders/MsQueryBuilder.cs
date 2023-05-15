using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;

namespace Common.Core.QueryBuilders;

public abstract class QueryBuilder
{
    private readonly QueryBuilderOptions _options = new QueryBuilderOptions();
    private readonly ICollection<RootQueryBuilder> _builders = new List<RootQueryBuilder>();
    protected void Add(RootQueryBuilder builder)
        => _builders.Add(builder);
    public void Build(out QueryBuilderOptions options)
    {
        options = _options;
        foreach (var item in _builders) 
            item.Build(_options);
    }

    public abstract DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner);
    public DeleteQueryBuilder<T> Delete<T>() => Delete<T>(null);
    public override string ToString() => _options.StringBuilder.ToString();
}

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
