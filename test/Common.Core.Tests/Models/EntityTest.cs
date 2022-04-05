using Common.Core.Models;

namespace Common.Core.Tests.Models;

public class EntityTest
{
    public static IEnumerable<object[]> Entities =>
        new List<object[]>
        {
            new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), true },
            new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), false },
            new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T20:15:37.9043446Z"), false },
            new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), DateTime.Parse("2022-02-09T19:15:37.9043446Z"), Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), DateTime.Parse("2022-02-09T20:15:37.9043446Z"), false }
        };

    [Theory]
    [MemberData(nameof(Entities))]
    public void Equals_CompareTwoObjects(Guid id1, DateTime timestamp1, Guid id2, DateTime timestamp2, bool expected)
    {
        var entity1 = new Entity { Id = id1, Timestamp = timestamp1 };
        var entity2 = new Entity { Id = id2, Timestamp = timestamp2 };

        bool resultEqual = entity1 == entity2;
        bool resultEqualHashCode = entity1.GetHashCode() == entity2.GetHashCode();

        Assert.Equal(expected: expected, actual: resultEqual);
        Assert.Equal(expected: expected, actual: resultEqualHashCode);
    }
}