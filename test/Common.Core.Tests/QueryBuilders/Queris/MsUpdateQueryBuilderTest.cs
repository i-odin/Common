using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class MsUpdateQueryBuilderTest
{

    [Theory]
    [InlineData("\r\nupdate dbo.TestClass")]
    public void Update_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new MsUpdateQueryBuilder<TestClass>().Update().Build(opt);
        Assert.Equal(expected, opt.ToString());
    }

    [Theory]
    [InlineData("\r\nupdate dbo.TestClass\r\nupdate dbo.TestClass")]
    public void UpdateUpdate_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new MsUpdateQueryBuilder<TestClass>().Update().Update().Build(opt);
        Assert.Equal(expected, opt.ToString());
    }

    [Theory]
    [InlineData("\r\nupdate dbo.TestClass\r\nwhere Id = @Id0 and Name = @Name1 and Age = @Age2 and Timespan = @Timespan3")]
    public void UpdateWhere_BuildSql(string expected)
    {
        var opt = new QueryBuilderOptions();
        new MsUpdateQueryBuilder<TestClass>()
           .Update()
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)))
           .Build(opt);
        Assert.Equal(expected, opt.ToString());
    }
}
