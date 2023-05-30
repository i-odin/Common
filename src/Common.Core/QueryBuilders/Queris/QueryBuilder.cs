using System.Text;

namespace Common.Core.QueryBuilders.Queris;

public abstract class QueryBuilder
{
    protected readonly QueryBuilderSource _source;
    public QueryBuilder(QueryBuilderSource source) { _source = source; } 
}

public class QueryBuilderSource
{
    public StringBuilder Query = new StringBuilder();
    public Parameters Parameters = new Parameters();
    public override string ToString()
        => Query.ToString();
}
