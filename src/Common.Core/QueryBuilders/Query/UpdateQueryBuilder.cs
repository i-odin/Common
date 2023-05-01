using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IUpdateQueryBuilder
{
    IUpdateQueryBuilder Update<T>(Action<IUpdateTranslator<T>> inner) where T : class;
    IUpdateQueryBuilder Join<T>(Action<IJoinTranslator<T>> inner) where T : class;
    IUpdateQueryBuilder Where<T>(Action<IWhereTranslator<T>> inner) where T : class;
}

public class UpdateQueryBuilder : QueryBuilder, IUpdateQueryBuilder
{
    public UpdateQueryBuilder(StringBuilder sb) : base(sb) {}

    public static UpdateQueryBuilder Create<T>(StringBuilder sb, Action<IUpdateTranslator<T>> inner)
        where T : class
        => (UpdateQueryBuilder)new UpdateQueryBuilder(sb).Update(inner);

    IUpdateQueryBuilder IUpdateQueryBuilder.Update<T>(Action<IUpdateTranslator<T>> inner)
    {
        Update(inner);
        return this;
    }

    IUpdateQueryBuilder IUpdateQueryBuilder.Where<T>(Action<IWhereTranslator<T>> inner) where T : class
    {
        Where(inner);
        return this;
    }

    IUpdateQueryBuilder IUpdateQueryBuilder.Join<T>(Action<IJoinTranslator<T>> inner)
    {
        throw new NotImplementedException();
    }
}