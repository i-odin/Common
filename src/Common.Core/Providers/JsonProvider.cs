using Common.Core.Extensions;
using Common.Core.Models;
using Common.Core.Wrappers;

namespace Common.Core.Providers;

public interface IFileProvider<TEntity> : IStorageProvider<TEntity>
    where TEntity : IHasId
{
    public string Path { get; }
}

public abstract class JsonProvider<TEntity> : IFileProvider<TEntity>
    where TEntity : IHasId
{
    private readonly IFileWrapper _fileWrapper;
    private readonly ISerializerWrapper _serializer;

    public string Path { get; }

    protected JsonProvider(string path) : this(path, new FileWrapper()) { }
    protected JsonProvider(string path, IFileWrapper fileWrapper) : this(path, fileWrapper, new JsonTextSerializerWrapper()) { }
    protected JsonProvider(string path, IFileWrapper fileWrapper, ISerializerWrapper serializer)
    {
        Path = path;
        _fileWrapper = fileWrapper;
        _serializer = serializer;
    }
    
    public void Add(TEntity item)
    {
        if (Read() is not ICollection<TEntity> collection) return;
        if (collection.Contains(item)) return;
        collection.Add(item);
        Write(collection);
    }

    public void Remove(TEntity item)
    {
        if (Read() is not ICollection<TEntity> collection) return;
        if (collection.Contains(item) == false) return;
        collection.Remove(item);
        Write(collection);
    }

    public IReadOnlyCollection<TEntity> Read()
    {
        var content = _fileWrapper.ReadAllText(Path);
        return content.IsEmpty() ? new List<TEntity>(0) : _serializer.Deserialize<IReadOnlyCollection<TEntity>>(content) ?? new List<TEntity>(0);
    }

    private void Write(ICollection<TEntity> source) => _fileWrapper.WriteAllText(Path, _serializer.Serialize(source));
}