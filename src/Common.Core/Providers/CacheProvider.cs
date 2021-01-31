using System.Collections.Generic;
using Common.Core.Models;

namespace Common.Core.Providers
{
    public abstract class CacheProvider<TCollection, TEntity> : ICacheProvider<TCollection, TEntity>
        where TCollection : ICollection<TEntity>, new()
        where TEntity : IHasId
    {
        private bool _initializeCollection;
        private TCollection _collection = new();
        private readonly IProvider<TEntity> _provider;
        
        protected CacheProvider(IStorageProvider<TEntity> provider)
        {
            _provider = provider;
        }

        public void Add(TEntity item)
        {
            InitializeCollection();
            if (_collection.Contains(item)) return;
            _collection.Add(item);
            _provider.Add(item);
        }

        public void Remove(TEntity item)
        {
            InitializeCollection();
            if (_collection.Contains(item) == false) return;
            _collection.Remove(item);
            _provider.Remove(item);
        }

        public IReadOnlyCollection<TEntity> Read()
        {
            InitializeCollection();
            return _collection as IReadOnlyCollection<TEntity>;
        }

        private void InitializeCollection()
        {
            if (_initializeCollection == false)
            {
                _collection = (TCollection)_provider.Read();
                _initializeCollection = true;
            }
        }
    }
}
