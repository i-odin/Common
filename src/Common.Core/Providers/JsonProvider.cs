using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Wrappers;

namespace Common.Core.Providers
{
    public interface IJsonProvider<TEntity> : IStorageProvider<TEntity>
        where TEntity : IHasId
    {
        public string Path { get; }
    }

    public abstract class JsonProvider<TEntity> : IJsonProvider<TEntity>
        where TEntity : IHasId
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly ISerializerWrapper _serializer;

        public string Path { get; }

        protected JsonProvider(string path) : this(path, FileWrapper.Create()) { }
        protected JsonProvider(string path, IFileWrapper fileWrapper) : this(path, fileWrapper, JsonTextSerializerWrapper.Create()) { }
        protected JsonProvider(string path, IFileWrapper fileWrapper, ISerializerWrapper serializer)
        {
            Path = path;
            _fileWrapper = fileWrapper;
            _serializer = serializer;
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

        public IReadOnlyCollection<TEntity> Read() => _serializer.Deserialize<IReadOnlyCollection<TEntity>>(_fileWrapper.ReadAllText(Path));

        private void Write(ICollection<TEntity> source) => _fileWrapper.WriteAllText(Path, _serializer.Serialize(source));
    }
}
