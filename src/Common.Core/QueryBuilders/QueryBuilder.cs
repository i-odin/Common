using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;
using System.Text;

namespace Common.Core.QueryBuilders;

public class QueryBuilderOptions
{
    public StringBuilder StringBuilder { get; set; } = new StringBuilder();
    public Parameters Parameters { get; set; } = new Parameters();
    public override string ToString()
        => StringBuilder.ToString();
}

public abstract class QueryBuilder
{
    private readonly QueryBuilderOptions _options = new QueryBuilderOptions();
    private readonly ICollection<BaseQueryBuilder> _builders = new List<BaseQueryBuilder>();
    protected void Add(BaseQueryBuilder builder)
        => _builders.Add(builder);
    
    public abstract DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner);
    public DeleteQueryBuilder<T> Delete<T>() => Delete<T>(null);

    public override string ToString()
    {
        //TODO: Переделать
        foreach (var item in _builders)
            item.Build(_options);

        return _options.StringBuilder.ToString();
    }
}

public static class QueryBuilderExtension
{
    public static void UseDapper(this QueryBuilder builder)
    {
        throw new NotImplementedException();
    }
}
