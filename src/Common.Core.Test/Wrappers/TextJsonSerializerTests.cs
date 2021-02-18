using Common.Core.Models;
using Common.Core.Wrappers;
using Xunit;

namespace Common.Core.Test.Wrappers
{
    public class TextJsonSerializerTests
    {
        [Fact]
        public void TextJsonSerializeSerialize()
        {
            var entityTest = new EntityTest { Id = 1 };
            var serializer = new JsonTextSerializer();

            var result = serializer.Serialize(entityTest);

            Assert.Equal(expected: "{\"Id\":1}", actual: result);
        }

        [Fact]
        public void TextJsonSerializeDeserialize()
        {
            var serializer = new JsonTextSerializer();

            var result = serializer.Deserialize<EntityTest>("{\"Id\":1}");

            Assert.Equal(expected: 1, actual: result.Id);
        }

        private class EntityTest : HasIdBase<int> { }
    }
}
