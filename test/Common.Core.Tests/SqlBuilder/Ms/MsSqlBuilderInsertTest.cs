using Common.Core.QueryBuilders.Ms;

namespace Common.Core.Tests.SqlBuilder.Ms;

public class MsSqlBuilderInsertTest
{
    [Theory]
    [InlineData("insert into TestClass (Id, Timespan, Name, Age)\r\nvalues ('00000000-0000-0000-0000-000000000000', '2023-04-30T00:00:00.0000000', null, 10)")]
    public void Insert_BuildInsertSql(string expected)
    {
        var builder = new MsQueryBuilder()
            .Insert<TestClass>(x => x.Values(y => y.Id, Guid.Empty)
                                     .Values(y => y.Timespan, new DateTime(2023, 04, 30))
                                     .Values(y => y.Name, null)
                                     .Values(y => y.Age, 10));

        Assert.Equal(expected, builder.ToString());
    }
}
