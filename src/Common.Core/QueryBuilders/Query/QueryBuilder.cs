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
        var obj = (InsertTranslator<T>)_sb;
        obj.Insert();
        inner(obj);
        obj.InsertEnd();
        return this;
    }

    public QueryBuilder<T> Update(Action<UpdateTranslator<T>> inner)
    {
        inner((UpdateTranslator<T>)_sb);
        return this;
    }

    public QueryBuilder<T> Delete()
    {
        var _ = (DeleteTranslator<T>)_sb;
        return this;
    }

    public QueryBuilder<T> Where(Action<WhereTranslator<T>> inner) 
    {
        inner((WhereTranslator<T>)((WhereTranslator<T>)_sb).Where());
        return this;
    }

    public QueryBuilder<T> Join<T1>(Action<JoinTranslator<T>> inner) 
    {
        inner((JoinTranslator<T>)((JoinTranslator<T>)_sb).Join());
        return this;
    }
}
