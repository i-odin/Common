using Common.Core.Models;
using Common.Core.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Tests.SqlBuilder
{
    public class MsSqlBuilderTest
    {
        [Theory]
        [InlineData(@"update TestClass 
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'")]
        public void Update_BuildUpdateSql(string expected)
        {
            var builder = new MsSqlBuilder().Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                                                     .Set(y => y.Name, null)
                                                                     .Set(y => y.Age, 10)
                                                                     .Set(y => y.Timespan, new DateTime(2023, 04, 23)));
            Assert.Equal(expected, builder.ToString());
        }
    }

    public class TestClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime Timespan { get; set; }
    }
}
