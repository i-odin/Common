using Common.Core.Models;
using Common.Core.Providers;

namespace Common.Core.Tests.Providers;

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
        var entityTest = new MyEntity { Id = Entity.NewId()};
        var cacheProvider = new MockListStorageProvider();

        cacheProvider.Add(entityTest);
        var collectionCount = cacheProvider.Read().Count;

        Assert.Equal(expected: 2, actual: collectionCount);
    }

    [Fact]
    public void Remove_RemoveObject_ReturnZeroCount()
    {
        var entityTest = new MyEntity { Id = StabStorageProvider.Id };
        var cacheProvider = new MockListStorageProvider();

        cacheProvider.Remove(entityTest);
        var collectionCount = cacheProvider.Read().Count;

        Assert.Equal(expected: 0, actual: collectionCount);
    }

    private class MockListStorageProvider : ListStorageProvider<MyEntity>
    {
        public MockListStorageProvider() : base(new StabStorageProvider())
        {
        }
    }

    //TODO: Использовать библиотеку Mock
    private class StabStorageProvider : IStorageProvider<MyEntity>
    {
        public static Guid Id = MyEntity.NewId();

        public void Add(MyEntity item) { }

        public void Remove(MyEntity item) { }

        public IReadOnlyCollection<MyEntity> Read()
        {
            return new List<MyEntity> { new() { Id = Id } };
        }
    }

    public class MyEntity : Entity { }
}