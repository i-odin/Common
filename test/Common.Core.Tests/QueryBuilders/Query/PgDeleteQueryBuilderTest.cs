using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.QueryBuilders.Query;

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
    [InlineData("\r\ndelete from test.Test")]
    public void DeleteTableNameAndSchema_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new PgDeleteQueryBuilder<TestClass>().Delete(x => x.WithTable("Test").WithSchema("test")).Build(opt);
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

    [Theory]
    [InlineData("\r\ndelete from public.Test\r\ndelete from public.Test")]
    public void DoubleDeleteTableNameAndSchema_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new PgDeleteQueryBuilder<TestClass>()
            .Delete("Test")
            .Delete(x => x.WithTable("Test"))
            .Build(opt);
        Assert.Equal(expected, opt.ToString());
    }
}
