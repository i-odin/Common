using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IUpdateQueryBuilder<T>
    where T : class
{
    IUpdateQueryBuilder<T> Update(Action<IUpdateTranslator<T>> inner);
    IUpdateQueryBuilder<T> Join(Action<IJoinTranslator<T>> inner);
    IUpdateQueryBuilder<T> Where(Action<IWhereTranslator<T>> inner);
}

public class UpdateQueryBuilder<T> : QueryBuilder<T>, IUpdateQueryBuilder<T>
    where T : class
{
    public UpdateQueryBuilder(StringBuilder sb) : base(sb) {}

    public static UpdateQueryBuilder<T> Create(StringBuilder sb, Action<IUpdateTranslator<T>> inner)
        => (UpdateQueryBuilder<T>)new UpdateQueryBuilder<T>(sb).Update(inner);

    IUpdateQueryBuilder<T> IUpdateQueryBuilder<T>.Update(Action<IUpdateTranslator<T>> inner)
    {
        Update(inner);
        return this;
    }

    IUpdateQueryBuilder<T> IUpdateQueryBuilder<T>.Where(Action<IWhereTranslator<T>> inner)
    {
        Where(inner);
        return this;
    }

    IUpdateQueryBuilder<T> IUpdateQueryBuilder<T>.Join(Action<IJoinTranslator<T>> inner)
    {
        throw new NotImplementedException();
    }
}