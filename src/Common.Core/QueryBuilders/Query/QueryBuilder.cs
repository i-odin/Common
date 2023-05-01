using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public class QueryBuilder
{
    private readonly StringBuilder _sb;
    public QueryBuilder(StringBuilder sb) { _sb = sb; }
    public override string ToString() => _sb.ToString();

    public QueryBuilder Insert<T>(Action<InsertTranslator<T>> inner)
        where T : class
    {
        var obj = (InsertTranslator<T>)_sb;
        obj.Insert();
        inner(obj);
        obj.InsertEnd();
        return this;
    }

    public QueryBuilder Update<T>(Action<UpdateTranslator<T>> inner) 
        where T : class
    {
        inner((UpdateTranslator<T>)_sb);
        return this;
    }

    public QueryBuilder Delete<T>()
        where T : class
    {
        var _ = (DeleteTranslator<T>)_sb;
        return this;
    }

    public QueryBuilder Where<T>(Action<WhereTranslator<T>> inner) 
        where T : class
    {
        inner(((WhereTranslator<T>)_sb).Where());
        return this;
    }

    public QueryBuilder Join<T>(Action<JoinTranslator<T>> inner) 
        where T : class
    {
        inner(((JoinTranslator<T>)_sb).Join());
        return this;
    }
}
