using System.Collections.Generic;
using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IProvider<TEntity>
        where TEntity : IHasId
    {
        void Add(TEntity item);
        void Remove(TEntity item);
        ICollection<TEntity> Read();
    }
}
