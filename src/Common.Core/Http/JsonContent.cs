using System.Net.Http;
using System.Text;
using System.Text.Json;
using Common.Core.Helper;

namespace Common.Core.Http
{
    public class JsonContent<T> : StringContent where T : class
    {
        public JsonContent(T value) : base(JsonSerializer.Serialize(value), Encoding.UTF8, MediaType.ApplicationJson)
        {
        }

        public JsonContent(T value, string mediaType) : base(JsonSerializer.Serialize(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
