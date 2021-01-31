using System.Collections.Generic;
using System.Text.Json;
using Common.Core.Models;
using Common.Core.Wrappers;

namespace Common.Core.Providers
{
    public abstract class JsonProvider<TEntity> : IJsonProvider<TEntity>
        where TEntity : IHasId
    {
        private readonly IFileWrapper _fileWrapper;

        public string Path { get; set; }

        //TODO: Фабрика?
        protected JsonProvider(string path) : this(path, new FileWrapper()) { }

        protected JsonProvider(string path, IFileWrapper fileWrapper)
        {
            Path = path;
            _fileWrapper = fileWrapper;
        }
        
        public void Add(TEntity item)
        {
            var collection = Read() as ICollection<TEntity>;
            if (collection.Contains(item)) return;
            collection.Add(item);
            Write(collection);
        }

        public void Remove(TEntity item)
        {
            var collection = Read() as ICollection<TEntity>;
            if (collection.Contains(item) == false) return;
            collection.Remove(item);
            Write(collection);
        }

        //TODO: Вынести JsonSerializer ?
        public IReadOnlyCollection<TEntity> Read() => JsonSerializer.Deserialize<IReadOnlyCollection<TEntity>>(_fileWrapper.ReadAllText(Path));

        private void Write(ICollection<TEntity> source) => _fileWrapper.WriteAllText(Path, JsonSerializer.Serialize(source));
    }
}
