using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Providers;
using Xunit;

namespace Common.Core.Test.Providers
{
    public class CacheProviderTests
    {
        [Fact]
        public void CacheProviderInitializeCollection()
        {
            var cacheProvider = new CacheProviderTest();

            var collectionCount = cacheProvider.Collection.Count;

            Assert.Equal(expected: 1, collectionCount);
        }

        [Fact]
        public void CacheProviderRead()
        {
            var cacheProvider = new CacheProviderTest();

            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 1, collectionCount);
        }

        [Fact]
        public void CacheProviderAdd()
        {
            var entityTest = new EntityTest {Id = 2};
            var cacheProvider = new CacheProviderTest();

            cacheProvider.Add(entityTest);
            var collectionCount = cacheProvider.Collection.Count;

            Assert.Equal(expected: 2, collectionCount);
        }

        [Fact]
        public void CacheProviderRemove()
        {
            var entityTest = new EntityTest { Id = 1 };
            var cacheProvider = new CacheProviderTest();

            cacheProvider.Remove(entityTest);
            var collectionCount = cacheProvider.Collection.Count;

            Assert.Equal(expected: 0, collectionCount);
        }

        private class CacheProviderTest : CacheProvider<List<EntityTest>, EntityTest>
        {
            public CacheProviderTest() : base(new StorageProviderMock())
            {
            }
        }

        private class EntityTest : HasIdBase<int> { }

        private class StorageProviderMock : IStorageProvider<EntityTest>
        {
            public void Add(EntityTest item) { }

            public void Remove(EntityTest item) { }

            public ICollection<EntityTest> Read()
            {
                return new List<EntityTest> { new() { Id = 1 } };
            }
        }
    }
}
