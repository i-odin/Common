using System;
using System.Collections.Generic;
using Common.Core.Models;
using Xunit;

namespace Common.Core.Tests.Models
{
    public class EntityTest
    {
        public static IEnumerable<object[]> Entities =>
            new List<object[]>
            {
                new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 123, Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 123, true },
                new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 123, Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), 123, false },
                new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 123, Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 321, false },
                new object[] { Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6450"), 123, Guid.Parse("62bd3e43-58c7-415a-a380-3c2b43da6451"), 321, false }
            };

        [Theory]
        [MemberData(nameof(Entities))]
        public void Equals_CompareTwoObjects(Guid obj1Id, long obj1Timestamp, Guid obj2Id, long obj2Timestamp, bool expected)
        {
            var entity1 = new Entity { Id = obj1Id, Timestamp =  obj1Timestamp };
            var entity2 = new Entity { Id = obj2Id, Timestamp =  obj2Timestamp };

            bool resultEqual = entity1 == entity2;
            bool resultEqualHashCode = entity1.GetHashCode() == entity2.GetHashCode();

            Assert.Equal(expected: expected, actual: resultEqual);
            Assert.Equal(expected: expected, actual: resultEqualHashCode);
        }
    }
}