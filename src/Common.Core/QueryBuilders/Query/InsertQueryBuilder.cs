using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IInsertQueryBuilder<T>
    where T : class
{
    IInsertQueryBuilder<T> Insert(Action<IInsertTranslator<T>> inner);
}

public class InsertQueryBuilder<T> : QueryBuilder<T>, IInsertQueryBuilder<T>
    where T : class
{
    public InsertQueryBuilder(StringBuilder sb) : base(sb) { }

    public InsertQueryBuilder<T> Insert(Action<InsertTranslator<T>> inner)
    {
        InsertTranslator<T>.Insert(_sb, inner);
        return this;
    }

    public static InsertQueryBuilder<T> Insert(StringBuilder sb, Action<IInsertTranslator<T>> inner) 
        => new InsertQueryBuilder<T>(sb).Insert(inner);
    IInsertQueryBuilder<T> IInsertQueryBuilder<T>.Insert(Action<IInsertTranslator<T>> inner)
    {
        Insert(inner);
        return this;
    }
}