using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class PgUpdateQueryBuilderTest
{
    [Theory]
    [InlineData("\r\nupdate public.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new PgUpdateQueryBuilder<TestClass>().Update().Build(source);
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\nupdate public.TestClass\r\nupdate public.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new PgUpdateQueryBuilder<TestClass>().Update().Update().Build(source);
        Assert.Equal(expected, source.ToString());
    }
}
