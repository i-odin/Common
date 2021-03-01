using System.Collections.Generic;
using Common.Core.Models;
using Common.Core.Providers;
using Xunit;

namespace Common.Core.Test.Providers
{
    public class ListStorageProviderTests
    {
        [Fact]
        public void Read_ReadAll_ReturnOneCount()
        {
            var cacheProvider = new ListStorageProviderTest();

            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 1, actual: collectionCount);
        }

        [Fact]
        public void Add_AddObject_ReturnTwoCount()
        {
            var entityTest = new EntityTest {Id = 2};
            var cacheProvider = new ListStorageProviderTest();

            cacheProvider.Add(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 2, actual: collectionCount);
        }

        [Fact]
        public void Remove_RemoveObject_ReturnZeroCount()
        {
            var entityTest = new EntityTest { Id = 1 };
            var cacheProvider = new ListStorageProviderTest();

            cacheProvider.Remove(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 0, actual: collectionCount);
        }

        private class ListStorageProviderTest : ListStorageProvider<EntityTest>
        {
            public ListStorageProviderTest() : base(new StabStorageProvider())
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