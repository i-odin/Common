using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core.Json;

namespace Common.Core.Collection
{
    public interface IJsonCollection<T> 
        where T : class
    {
        Task InitializeAsync();
        Task AddAsync(T item);
        Task RemoveAsync(T item);
        IReadOnlyCollection<T> GetAll();
        Task<IReadOnlyCollection<T>> GetAllAsync();
    }

    public abstract class JsonCollection<T> : IJsonCollection<T>, IReadWriteJson<T> 
        where T : class
    {
        protected readonly List<T> Collection = new();

        public abstract string Path { get; }

        public virtual async Task AddAsync(T item)
        {
            if (item == null)
                return;

            if (Collection.Contains(item) == false)
            {
                Collection.Add(item);
                var collection = await ((IReadWriteJson<T>)this).ReadAsync();
                if (collection.Contains(item) == false)
                {
                    collection.Add(item);
                    await ((IReadWriteJson<T>)this).WriteAsync(collection);
                }
            }
        }
        
        public virtual async Task RemoveAsync(T item)
        {
            if (item == null)
                return;

            if (Collection.Contains(item))
            {
                Collection.Remove(item);
                var collection = await ((IReadWriteJson<T>)this).ReadAsync();
                if (collection.Contains(item))
                {
                    collection.Remove(item);
                    await ((IReadWriteJson<T>)this).WriteAsync(collection);
                }
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync() => Collection;
        public IReadOnlyCollection<T> GetAll() => Collection;

        public virtual async Task InitializeAsync()
        {
            Collection.AddRange(await ((IReadWriteJson<T>)this).ReadAsync());
        }
    }
}
