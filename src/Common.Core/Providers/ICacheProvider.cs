using System.Collections.Generic;
using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface ICacheProvider<out TCollection, TEntity> : IProvider<TEntity>
        where TCollection : ICollection<TEntity>
        where TEntity : IHasId
    {
        public TCollection Collection { get; }
    }
}
