using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IStorageProvider<TEntity> : IProvider<TEntity>
        where TEntity : IHasId
    {
    }
}
