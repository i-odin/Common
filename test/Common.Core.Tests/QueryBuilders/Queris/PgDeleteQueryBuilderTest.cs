using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class PgDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete from public.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new PgDeleteQueryBuilder<TestClass>(source).Delete();
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete from public.TestClass\r\ndelete from public.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new PgDeleteQueryBuilder<TestClass>(source).Delete().Delete();
        Assert.Equal(expected, source.ToString());
    }
}
