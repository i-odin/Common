using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface ICacheProvider<TEntity> : IProvider<TEntity>
        where TEntity : IHasId
    {
    }
}
