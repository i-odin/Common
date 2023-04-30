using System.Text;

namespace Common.Core.QueryBuilders.Query;

public class QueryBuilder
{
    protected StringBuilder _sb;
    public QueryBuilder(StringBuilder sb) { _sb = sb; }
    public override string ToString() => _sb.ToString();
}
