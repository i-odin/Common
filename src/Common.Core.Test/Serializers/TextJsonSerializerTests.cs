using Common.Core.Models;
using Common.Core.Serializers;
using Xunit;

namespace Common.Core.Test.Serializers
{
    public class TextJsonSerializerTests
    {
        [Fact]
        public void TextJsonSerializeSerialize()
        {
            var entityTest = new EntityTest { Id = 1 };
            var serializer = new TextJsonSerializer();

            var result = serializer.Serialize(entityTest);

            Assert.Equal(expected: "{\"Id\":1}", actual: result);
        }

        [Fact]
        public void TextJsonSerializeDeserialize()
        {
            var serializer = new TextJsonSerializer();

            var result = serializer.Deserialize<EntityTest>("{\"Id\":1}");

            Assert.Equal(expected: 1, actual: result.Id);
        }

        private class EntityTest : HasIdBase<int> { }
    }
}
