using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IListStorageProviderProvider<TEntity> : IProvider<TEntity>
        where TEntity : IHasId
    {
    }
}
