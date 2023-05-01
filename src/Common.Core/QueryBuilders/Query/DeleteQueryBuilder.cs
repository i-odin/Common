using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IDeleteQueryBuilder
{
    IDeleteQueryBuilder Delete<T>() where T : class;
    IDeleteQueryBuilder Where<T>(Action<IWhereTranslator<T>> inner) where T : class;
}

public class DeleteQueryBuilder : QueryBuilder, IDeleteQueryBuilder
{
    public DeleteQueryBuilder(StringBuilder sb) : base(sb) {}

    public static DeleteQueryBuilder Create<T>(StringBuilder sb)
        where T : class
        => (DeleteQueryBuilder)new DeleteQueryBuilder(sb).Delete<T>();

    IDeleteQueryBuilder IDeleteQueryBuilder.Delete<T>()
    {
        Delete<T>();
        return this;
    }

    IDeleteQueryBuilder IDeleteQueryBuilder.Where<T>(Action<IWhereTranslator<T>> inner)
    {
        Where(inner);
        return this;
    }
}
