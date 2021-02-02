using System.Net.Http;
using System.Text;
using Common.Core.Helpers;
using Common.Core.Serializers;

namespace Common.Core.Http
{
    public class JsonContent<T> : StringContent where T : class
    {
        public JsonContent(T value) : this(value, new TextJsonSerializer(), MediaType.ApplicationJson)
        {
        }

        public JsonContent(T value, string mediaType) : this(value, new TextJsonSerializer(), mediaType)
        {
        }

        public JsonContent(T value, ISerializer serializer) : base(serializer.Serialize(value), Encoding.UTF8, MediaType.ApplicationJson)
        {
        }

        public JsonContent(T value, ISerializer serializer, string mediaType) : base(serializer.Serialize(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
