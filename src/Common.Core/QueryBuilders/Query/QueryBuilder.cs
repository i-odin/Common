using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public class QueryBuilder<T>
    where T : class
{
    protected readonly StringBuilder _sb;
    public QueryBuilder(StringBuilder sb) { _sb = sb; }
    public override string ToString() => _sb.ToString();

    public QueryBuilder<T> Where(Action<WhereTranslator<T>> inner) 
    {
        WhereTranslator<T>.Where(_sb, inner);
        return this;
    }

    public QueryBuilder<T> Join<T1>(Action<JoinTranslator<T>> inner) 
    {
        JoinTranslator<T>.Join(_sb, inner);
        return this;
    }
}
