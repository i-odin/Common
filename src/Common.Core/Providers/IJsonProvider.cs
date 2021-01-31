using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IJsonProvider<TEntity> : IFileProvider<TEntity>
        where TEntity : IHasId
    {
    }
}
