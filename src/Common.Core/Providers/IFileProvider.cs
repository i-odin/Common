using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IFileProvider<TEntity> : IStorageProvider<TEntity>
        where TEntity : IHasId 
    {
        public string Path { get; set; }
    }
}
