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

    public InsertQueryBuilder Insert<T>(Action<IInsertTranslator<T>> inner)
        where T : class
    {
        var obj = (InsertTranslator<T>)_sb;
        obj.Insert();
        inner(obj);
        obj.InsertEnd();
        return this;
    }

    public static InsertQueryBuilder Create<T>(StringBuilder sb, Action<IInsertTranslator<T>> inner) 
        where T : class
        => new InsertQueryBuilder(sb).Insert(inner);

    IInsertQueryBuilder IInsertQueryBuilder.Insert<T>(Action<IInsertTranslator<T>> inner)
    {
        Insert(inner);
        return this;
    }
}