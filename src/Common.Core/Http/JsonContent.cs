using System.Net.Http;
using System.Text;
using Common.Core.Helpers;
using Common.Core.Wrappers;

namespace Common.Core.Http
{
    public class JsonContent<T> : StringContent where T : class
    {
        public JsonContent(T value) : this(value, new JsonTextSerializerWrapper(), MediaTypeHelper.ApplicationJson)
        {
        }

        public JsonContent(T value, string mediaType) : this(value, new JsonTextSerializerWrapper(), mediaType)
        {
        }

        public JsonContent(T value, ISerializerWrapper serializer) : base(serializer.Serialize(value), Encoding.UTF8, MediaTypeHelper.ApplicationJson)
        {
        }

        public JsonContent(T value, ISerializerWrapper serializer, string mediaType) : base(serializer.Serialize(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
