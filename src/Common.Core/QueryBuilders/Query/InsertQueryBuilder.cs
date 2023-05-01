using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IInsertQueryBuilder
{
    IInsertQueryBuilder Insert<T>(Action<IInsertTranslator<T>> inner) where T : class;
}

public class InsertQueryBuilder : QueryBuilder, IInsertQueryBuilder
{
    public InsertQueryBuilder(StringBuilder sb) : base(sb) { }

    public static InsertQueryBuilder Create<T>(StringBuilder sb, Action<IInsertTranslator<T>> inner) 
        where T : class
        => (InsertQueryBuilder)new InsertQueryBuilder(sb).Insert(inner);

    IInsertQueryBuilder IInsertQueryBuilder.Insert<T>(Action<IInsertTranslator<T>> inner)
    {
        Insert(inner);
        return this;
    }
}