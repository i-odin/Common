using System;
using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Wrappers;
using Xunit;

namespace Common.Core.Tests.Wrappers
{
    public class JsonTextSerializerWrapperTest
    {
        [Theory]
        [MemberData(nameof(SerializeData))]
        public void Serialize_EntityTestToString_ReturnTrue(Guid? input, string expected)
        {
            Entity entity = null;
            if (input.HasValue)
                entity = new Entity { Id = input.Value };
            var serializer = new JsonTextSerializerWrapper();

            var result = serializer.Serialize(entity);

            Assert.Equal(expected: expected, actual: result);
        }
        

        [Theory]
        [MemberData(nameof(DeserializeData))]
        public void Deserialize_StringToEntityTest_ReturnTrue(string input, Guid? expected)
        {
            var serializer = new JsonTextSerializerWrapper();
            
            var result = serializer.Deserialize<Entity>(input);

            Assert.Equal(expected, actual: result?.Id);
        }

        public static IEnumerable<object[]> SerializeData =>
            new List<object[]>
            {
                new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), "{\"Id\":\"62bd3e43-58c7-415a-a380-3c2b43da6450\"}" },
                new object[] { null , "" }
            };

        public static IEnumerable<object[]> DeserializeData =>
            new List<object[]>
            {
                new object[] { "{\"Id\":\"62bd3e43-58c7-415a-a380-3c2b43da6450\"}", Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450") },
                new object[] { "", null },
                new object[] { " ", null }
            };
    }
}
