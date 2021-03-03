using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Providers;
using Xunit;

namespace Common.Core.Tests.Providers
{
    public class ListStorageProviderTest
    {
        [Fact]
        public void Read_ReadAll_ReturnOneCount()
        {
            var cacheProvider = new MockListStorageProvider();

            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 1, actual: collectionCount);
        }

        [Fact]
        public void Add_AddObject_ReturnTwoCount()
        {
            var entityTest = new EntityTest {Id = 2};
            var cacheProvider = new MockListStorageProvider();

            cacheProvider.Add(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 2, actual: collectionCount);
        }

        [Fact]
        public void Remove_RemoveObject_ReturnZeroCount()
        {
            var entityTest = new EntityTest { Id = 1 };
            var cacheProvider = new MockListStorageProvider();

            cacheProvider.Remove(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 0, actual: collectionCount);
        }

        private class MockListStorageProvider : ListStorageProvider<EntityTest>
        {
            public MockListStorageProvider() : base(new StabStorageProvider())
            {
            }
        }

        private class EntityTest : HasId<int> { }

        //TODO: Использовать библиотеку Mock
        private class StabStorageProvider : IStorageProvider<EntityTest>
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