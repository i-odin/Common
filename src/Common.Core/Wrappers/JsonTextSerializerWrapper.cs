using System.Text.Json;
using Common.Core.Extensions;

namespace Common.Core.Wrappers;

public interface ISerializerWrapper
{
    public string Serialize<T>(T source);
    public T? Deserialize<T>(string source);
}

public class JsonTextSerializerWrapper : ISerializerWrapper
{
    public string Serialize<T>(T source) => source == null ? string.Empty : JsonSerializer.Serialize(source);
    public T? Deserialize<T>(string source) => source.IsEmpty() ? default : JsonSerializer.Deserialize<T>(source);
}