using Common.Core.Models;
using Common.Core.Wrappers;
using Xunit;

namespace Common.Core.Tests.Wrappers
{
    public class JsonTextSerializerWrapperTest
    {
        [Theory]
        [InlineData(1, "{\"Id\":1}")]
        [InlineData(null, "")]
        public void Serialize_EntityTestToString_ReturnTrue(int? input, string expected)
        {
            EntityTest entityTest = null;
            if (input.HasValue)
                entityTest = new EntityTest { Id = input.Value };
            var serializer = new JsonTextSerializerWrapper();

            var result = serializer.Serialize(entityTest);

            Assert.Equal(expected: expected, actual: result);
        }

        [Theory]
        [InlineData("{\"Id\":1}", 1)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        public void Deserialize_StringToEntityTest_ReturnTrue(string input, int? expected)
        {
            var serializer = new JsonTextSerializerWrapper();

            var result = serializer.Deserialize<EntityTest>(input);

            Assert.Equal(expected, actual: result?.Id);
        }

        private class EntityTest : HasId<int> { }
    }
}
