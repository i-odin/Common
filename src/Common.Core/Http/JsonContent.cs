using System.Net.Http;
using System.Text;
using Common.Core.Helpers;
using Common.Core.Wrappers;

namespace Common.Core.Http
{
    public class JsonContent<T> : StringContent where T : class
    {
        private JsonContent(string content) : base(content, Encoding.UTF8, MediaTypeHelper.ApplicationJson) { }
        public JsonContent(T value) : this(value, JsonTextSerializerWrapper.Create()) { }
        public JsonContent(T value, ISerializerWrapper serializer) : this(serializer.Serialize(value)) { }
    }
}
