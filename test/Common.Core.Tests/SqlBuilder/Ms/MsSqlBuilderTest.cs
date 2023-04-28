using Common.Core.SqlBuilder;

namespace Common.Core.Tests.SqlBuilder.Ms
{
    public class MsSqlBuilderTests
    {
        [Theory]
        [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
        public void UpdateWhere_BuildUpdateWhereSql(string expected)
        {
            var builder = new MsSqlQueryBuilder()
                .Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                         .Set(y => y.Name, null)
                                         .Set(y => y.Age, 10)
                                         .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
                .Where<TestClass>(x => x.Equal(y => y.Id, Guid.Empty).And()
                                        .Equal(y => y.Name, null).Or()
                                        .Equal(y => y.Age, 10).And()
                                        .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
            Assert.Equal(expected, builder.ToString());
        }

        [Theory]
        [InlineData(@"delete TestClass
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
        public void DeleteWhere_BuildUpdateWhereSql(string expected)
        {
            var builder = new MsSqlQueryBuilder()
                .Delete<TestClass>()
                .Where<TestClass>(x => x.Equal(y => y.Id, Guid.Empty).And()
                                        .Equal(y => y.Name, null).Or()
                                        .Equal(y => y.Age, 10).And()
                                        .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
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
