using Common.Core.Models;
using Common.Core.Providers;
using Common.Core.Wrappers;
using Xunit;

namespace Common.Core.Test.Providers
{
    public class JsonProviderTests
    {
        [Fact]
        public void Add_AddObject_ReturnOneCount()
        {
            var entityTest = new EntityTest {Id = 1};
            var jsonProvider = new JsonProviderTest();
            
            jsonProvider.Add(entityTest);
            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 1, actual: jsonCount);
        }

        [Fact]
        public void Remove_AddAndRemoveObject_ReturnZeroCount()
        {
            var entityTest = new EntityTest { Id = 1 };
            var jsonProvider = new JsonProviderTest();

            jsonProvider.Add(entityTest);
            jsonProvider.Remove(entityTest);
            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 0, actual: jsonCount);
        }

        [Fact]
        public void Read_ReadAllFile_ReturnZeroCount()
        {
            var jsonProvider = new JsonProviderTest();

            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 0, actual: jsonCount);
        }

        private class EntityTest : HasId<int> { }

        private class JsonProviderTest : JsonProvider<EntityTest>
        {
            public JsonProviderTest() : this(string.Empty, new StubFileWrapper()) { }
            private JsonProviderTest(string path, IFileWrapper fileWrapper) : base(path, fileWrapper) { }
        }

        //TODO: Использовать библиотеку Mock
        private class StubFileWrapper : IFileWrapper
        {
            private string _source = string.Empty;
            public string ReadAllText(string path) => _source;
            public void WriteAllText(string path, string content) => _source = content;
        }
    }
}
