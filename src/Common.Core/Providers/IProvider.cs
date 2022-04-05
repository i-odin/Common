using Common.Core.Models;

namespace Common.Core.Providers;

public interface IProvider<TEntity>
    where TEntity : IHasId
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    IReadOnlyCollection<TEntity> Read();
}