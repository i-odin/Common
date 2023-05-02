using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public class QueryBuilder<T>
    where T : class
{
    private readonly StringBuilder _sb;
    public QueryBuilder(StringBuilder sb) { _sb = sb; }
    public override string ToString() => _sb.ToString();

    public QueryBuilder<T> Insert(Action<InsertTranslator<T>> inner)
    {
        InsertTranslator<T>.Insert(_sb, inner);
        return this;
    }

    public QueryBuilder<T> Update(Action<UpdateTranslator<T>> inner)
    {
        UpdateTranslator<T>.Update(_sb, inner);
        return this;
    }

    public QueryBuilder<T> Delete()
    {
        DeleteTranslator<T>.Delete(_sb);
        return this;
    }

    public QueryBuilder<T> Where(Action<WhereTranslator<T>> inner) 
    {
        WhereTranslator<T>.Where(_sb, inner);
        return this;
    }

    public QueryBuilder<T> Join<T1>(Action<JoinTranslator<T>> inner) 
    {
        //inner((JoinTranslator<T>)((JoinTranslator<T>)_sb).Join());
        return this;
    }
}
