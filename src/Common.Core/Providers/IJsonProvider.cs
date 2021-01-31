using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IJsonProvider<TEntity> : IStorageProvider<TEntity>
        where TEntity : IHasId
    {
        public string Path { get; set; }
    }
}
