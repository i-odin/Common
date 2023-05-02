using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public interface IUpdateQueryBuilder<T>
    where T : class
{
    IUpdateQueryBuilder<T> Update(Action<IUpdateTranslator<T>> inner);
    IUpdateQueryBuilder<T> Where(Action<IWhereTranslator<T>> inner);
    IUpdateQueryBuilder<T> Join<TJoin>(Action<IJoinTranslator<T, TJoin>> inner) 
        where TJoin : class;
    IUpdateQueryBuilder<T> Join<TJoin1, TJoin2>(Action<IJoinTranslator<TJoin1, TJoin2>> inner) 
        where TJoin1 : class 
        where TJoin2 : class;
}

public class UpdateQueryBuilder<T> : QueryBuilder<T>, IUpdateQueryBuilder<T>
    where T : class
{
    public UpdateQueryBuilder(StringBuilder sb) : base(sb) {}

    public UpdateQueryBuilder<T> Update(Action<UpdateTranslator<T>> inner)
    {
        UpdateTranslator<T>.Update(_sb, inner);
        return this;
    }

    public override UpdateQueryBuilder<T> Join<TJoin>(Action<JoinTranslator<T, TJoin>> inner)
    {
        JoinTranslator<T, TJoin>.UpdateJoin(_sb, inner);
        return this;
    }

    public static UpdateQueryBuilder<T> Update(StringBuilder sb, Action<IUpdateTranslator<T>> inner)
        => new UpdateQueryBuilder<T>(sb).Update(inner);
    
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

    IUpdateQueryBuilder<T> IUpdateQueryBuilder<T>.Join<TJoin>(Action<IJoinTranslator<T, TJoin>> inner)
    {
        Join(inner);
        return this;
    }

    IUpdateQueryBuilder<T> IUpdateQueryBuilder<T>.Join<TJoin1, TJoin2>(Action<IJoinTranslator<TJoin1, TJoin2>> inner)
    {
        //TODO реализация
        throw new NotImplementedException();
    }
}