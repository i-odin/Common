using Common.Core.QueryBuilders.Query;
using System.Text;

namespace Common.Core.Tests.QueryBuilders.Query;

public class PgDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete from public.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var sb = new StringBuilder();
        new PgDeleteQueryBuilder<TestClass>().Delete().Build(sb);
        Assert.Equal(expected, sb.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete from test.Test")]
    public void DeleteTableNameAndSchema_BuildSql(string expected)
    {
        var sb = new StringBuilder();
        new PgDeleteQueryBuilder<TestClass>().Delete(x => x.WithTable("Test").WithSchema("test")).Build(sb);
        Assert.Equal(expected, sb.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete from public.TestClass\r\ndelete from public.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var sb = new StringBuilder();
        new PgDeleteQueryBuilder<TestClass>().Delete().Delete().Build(sb);
        Assert.Equal(expected, sb.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete from public.Test\r\ndelete from public.Test")]
    public void DoubleDeleteTableNameAndSchema_BuildSql(string expected)
    {
        var sb = new StringBuilder();
        new PgDeleteQueryBuilder<TestClass>()
            .Delete("Test")
            .Delete(x => x.WithTable("Test"))
            .Build(sb);
        Assert.Equal(expected, sb.ToString());
    }
}
