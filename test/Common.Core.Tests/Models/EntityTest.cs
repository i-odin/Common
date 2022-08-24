using Common.Core.Models;

namespace Common.Core.Tests.Models;

public class EntityTest
{
    public static IEnumerable<object[]> Entities =>
        new List<object[]>
        {
            new object[] { 
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                true
            },
            new object[] {
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                false
            },
            new object[] {
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T20:15:37.9043446Z") },
                false
            },
            new object[] {
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z"), Deleted = true },
                false
            },
            new object[] {
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), Timestamp = DateTime.Parse("2022-02-09T19:15:37.9043446Z") },
                new Entity { Id = Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), Timestamp = DateTime.Parse("2022-02-09T20:15:37.9043446Z"), Deleted = true },
                false
            }
        };

    [Theory]
    [MemberData(nameof(Entities))]
    public void Equals_CompareTwoObjects(Entity entityOne, Entity entitySecond, bool expected)
    {
        bool resultEqual = entityOne == entitySecond;
        bool resultEqualHashCode = entityOne.GetHashCode() == entitySecond.GetHashCode();

        Assert.Equal(expected: expected, actual: resultEqual);
        Assert.Equal(expected: expected, actual: resultEqualHashCode);
    }
}