using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IListStorageProvider<TEntity> : IProvider<TEntity>
        where TEntity : IHasId
    {
    }
}
