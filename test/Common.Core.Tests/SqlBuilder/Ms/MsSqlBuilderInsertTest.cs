using Common.Core.QueryBuilders.Ms;

namespace Common.Core.Tests.SqlBuilder.Ms;

public class MsSqlBuilderInsertTest
{
    /*[Theory]
    [InlineData("delete TestClass")]
    public void Delete_BuildDeleteSql(string expected)
    {
        var builder = new MsQueryBuilder()
            .Insert<TestClass>(x => x.Field(y => y.Id)
                                     .Field(y => y.Timespan)
                                     .Field(y => y.Name)
                                     .Field(y => y.Age))
            .Value<TestClass>(x => x.Field(y => y.Id));

        Assert.Equal(expected, builder.ToString());
    }*/
}
