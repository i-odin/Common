using System.Text.Json;

namespace Common.Core.Serializers
{
    public class TextJsonSerializer : ISerializer
    {
        public string Serialize<T>(T source) => JsonSerializer.Serialize(source);
        public T Deserialize<T>(string source) => JsonSerializer.Deserialize<T>(source);
    }
}
