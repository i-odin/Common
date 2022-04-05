using Common.Core.Models;
using Common.Core.Providers;
using Common.Core.Wrappers;

namespace Common.Core.Tests.Providers;

public class JsonProviderTest
{
    [Fact]
    public void Add_AddObject_ReturnOneCount()
    {
        var entityTest = new Entity { Id = Entity.NewId() };
        var jsonProvider = new MockJsonProvider();
        
        jsonProvider.Add(entityTest);
        var jsonCount = jsonProvider.Read().Count;

        Assert.Equal(expected: 1, actual: jsonCount);
    }

    [Fact]
    public void Remove_AddAndRemoveObject_ReturnZeroCount()
    {
        var entityTest = new Entity { Id = Entity.NewId() };
        var jsonProvider = new MockJsonProvider();

        jsonProvider.Add(entityTest);
        jsonProvider.Remove(entityTest);
        var jsonCount = jsonProvider.Read().Count;

        Assert.Equal(expected: 0, actual: jsonCount);
    }

    [Fact]
    public void Read_ReadAllFile_ReturnZeroCount()
    {
        var jsonProvider = new MockJsonProvider();

        var jsonCount = jsonProvider.Read().Count;

        Assert.Equal(expected: 0, actual: jsonCount);
    }
    
    private class MockJsonProvider : JsonProvider<Entity>
    {
        public MockJsonProvider() : this(string.Empty, new StubFileWrapper()) { }
        private MockJsonProvider(string path, IFileWrapper fileWrapper) : base(path, fileWrapper) { }
    }

    //TODO: Использовать библиотеку Mock
    private class StubFileWrapper : IFileWrapper
    {
        private string _source = string.Empty;
        public string ReadAllText(string path) => _source;
        public void WriteAllText(string path, string content) => _source = content;
    }
}