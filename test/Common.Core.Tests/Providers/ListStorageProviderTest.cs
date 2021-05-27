using System;
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
            var entityTest = new Entity {Id = Entity.NewId()};
            var cacheProvider = new MockListStorageProvider();

            cacheProvider.Add(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 2, actual: collectionCount);
        }

        [Fact]
        public void Remove_RemoveObject_ReturnZeroCount()
        {
            var entityTest = new Entity { Id = StabStorageProvider.Id };
            var cacheProvider = new MockListStorageProvider();

            cacheProvider.Remove(entityTest);
            var collectionCount = cacheProvider.Read().Count;

            Assert.Equal(expected: 0, actual: collectionCount);
        }

        private class MockListStorageProvider : ListStorageProvider<Entity>
        {
            public MockListStorageProvider() : base(new StabStorageProvider())
            {
            }
        }

        //TODO: Использовать библиотеку Mock
        private class StabStorageProvider : IStorageProvider<Entity>
        {
            public static Guid Id = Entity.NewId();

            public void Add(Entity item) { }

            public void Remove(Entity item) { }

            public IReadOnlyCollection<Entity> Read()
            {
                return new List<Entity> { new() { Id = Id } };
            }
        }
    }
}