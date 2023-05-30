using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class MsDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete dbo.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source).Delete();
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass\r\ndelete dbo.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source).Delete().Delete();
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass\r\nwhere Id = @Id0 and Name = @Name1 and Age = @Age2 and Timespan = @Timespan3")]
    public void DeleteWhere_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source)
           .Delete()
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, source.ToString());
    }
}