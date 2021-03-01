using System.Collections.Generic;
using Common.Core.Models;

namespace Common.Core.Providers
{
    public interface IListStorageProvider<TEntity> : IProvider<TEntity>
        where TEntity : IHasId
    {
    }

    public abstract class ListStorageProvider<TEntity> : IListStorageProvider<TEntity>
        where TEntity : IHasId
    {
        private bool _initializeCollection;
        private List<TEntity> _list = new();
        private readonly IProvider<TEntity> _provider;
        
        protected ListStorageProvider(IStorageProvider<TEntity> provider)
        {
            _provider = provider;
        }

        public void Add(TEntity item)
        {
            InitializeCollection();
            if (_list.Contains(item)) return;
            _list.Add(item);
            _provider.Add(item);
        }

        public void Remove(TEntity item)
        {
            InitializeCollection();
            if (_list.Contains(item) == false) return;
            _list.Remove(item);
            _provider.Remove(item);
        }

        public IReadOnlyCollection<TEntity> Read()
        {
            InitializeCollection();
            return _list;
        }

        private void InitializeCollection()
        {
            if (_initializeCollection == false)
            {
                _list = (List<TEntity>)_provider.Read();
                _initializeCollection = true;
            }
        }
    }
}
