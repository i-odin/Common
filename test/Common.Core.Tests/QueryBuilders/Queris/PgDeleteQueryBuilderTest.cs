using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class PgDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete from public.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new PgDeleteQueryBuilder<TestClass>().Delete().Build(opt);
        Assert.Equal(expected, opt.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete from public.TestClass\r\ndelete from public.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new PgDeleteQueryBuilder<TestClass>().Delete().Delete().Build(opt);
        Assert.Equal(expected, opt.ToString());
    }
}
