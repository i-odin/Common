using Common.Core.QueryBuilder;

namespace Common.Core.Tests.SqlBuilder.Ms
{
    public class MsSqlBuilderUpdateTest
    {
        [Theory]
        [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'")]

        public void Update_BuildUpdateSql(string expected)
        {
            var builder = new MsSqlQueryBuilder()
                .Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                        .Set(y => y.Name, null)
                                        .Set(y => y.Age, 10)
                                        .Set(y => y.Timespan, new DateTime(2023, 04, 23)));
            Assert.Equal(expected, builder.ToString());
        }
    }
}
