using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Providers;
using Xunit;

namespace Common.Core.Test.Providers
{
    public class ListStorageProviderTests
    {
        [Fact]
        public void ListStorageProviderRead()
        {
            var cacheProvider = new ListStorageProviderTest();

            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 1, actual: collectionCount);
        }

        [Fact]
        public void ListStorageProviderAdd()
        {
            var entityTest = new EntityTest {Id = 2};
            var cacheProvider = new ListStorageProviderTest();

            cacheProvider.Add(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 2, actual: collectionCount);
        }

        [Fact]
        public void ListStorageProviderRemove()
        {
            var entityTest = new EntityTest { Id = 1 };
            var cacheProvider = new ListStorageProviderTest();

            cacheProvider.Remove(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 0, actual: collectionCount);
        }

        private class ListStorageProviderTest : ListStorageProvider<EntityTest>
        {
            public ListStorageProviderTest() : base(new StorageProviderMock())
            {
            }
        }

        private class EntityTest : HasIdBase<int> { }

        private class StorageProviderMock : IStorageProvider<EntityTest>
        {
            public void Add(EntityTest item) { }

            public void Remove(EntityTest item) { }

            public IReadOnlyCollection<EntityTest> Read()
            {
                return new List<EntityTest> { new() { Id = 1 } };
            }
        }
    }
}
