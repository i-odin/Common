using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IDeleteQueryBuilder<T>
    where T : class
{
    IDeleteQueryBuilder<T> Delete();
    IDeleteQueryBuilder<T> Where(Action<IWhereTranslator<T>> inner);
}

public class DeleteQueryBuilder<T> : QueryBuilder<T>, IDeleteQueryBuilder<T>
    where T : class
{
    public DeleteQueryBuilder(StringBuilder sb) : base(sb) {}

    public static DeleteQueryBuilder<T> Create(StringBuilder sb)
        => (DeleteQueryBuilder<T>)new DeleteQueryBuilder<T>(sb).Delete();

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Delete()
    {
        Delete();
        return this;
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Where(Action<IWhereTranslator<T>> inner)
    {
        Where(inner);
        return this;
    }
}
