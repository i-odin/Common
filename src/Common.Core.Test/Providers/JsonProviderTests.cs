using Common.Core.Models;
using Common.Core.Providers;
using Common.Core.Wrappers;
using Xunit;

namespace Common.Core.Test.Providers
{
    public class JsonProviderTests
    {
        [Fact]
        public void JsonProviderAdd()
        {
            var entityTest = new EntityTest {Id = 1};
            var jsonProvider = new JsonProviderTest();
            
            jsonProvider.Add(entityTest);
            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 1, actual: jsonCount);
        }

        [Fact]
        public void JsonProviderRemove()
        {
            var entityTest = new EntityTest { Id = 1 };
            var jsonProvider = new JsonProviderTest();

            jsonProvider.Add(entityTest);
            jsonProvider.Remove(entityTest);
            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 0, actual: jsonCount);
        }

        [Fact]
        public void JsonProviderRead()
        {
            var jsonProvider = new JsonProviderTest();

            var jsonCount = jsonProvider.Read().Count;

            Assert.Equal(expected: 0, actual: jsonCount);
        }

        private class EntityTest : HasId<int> { }

        private class JsonProviderTest : JsonProvider<EntityTest>
        {
            public JsonProviderTest() : this(string.Empty, new FileWrapperMock()) { }
            private JsonProviderTest(string path, IFileWrapper fileWrapper) : base(path, fileWrapper) { }
        }

        private class FileWrapperMock : IFileWrapper
        {
            private string _source = string.Empty;
            public string ReadAllText(string path) => _source;
            public void WriteAllText(string path, string content) => _source = content;
        }
    }
}
