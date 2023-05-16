using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.Tests.QueryBuilders.Translators;

public class MsTableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest dbo.TestClass")]
    public void Table_Build(string expected)
    {
        var opt = new QueryBuilderOptions();
        MsTableTranslator<TestClass>.Make("test", null).Run(opt);
        Assert.Equal(expected, opt.ToString());
    }

    [Theory]
    [InlineData("\r\ntest test.Test as test")]
    public void Table_BuildTableAndSchemaAndAlias(string expected)
    {
        var opt = new QueryBuilderOptions();
        MsTableTranslator<TestClass>.Make("test", x => x.WithTable("Test").WithSchema("test").WithAlias("test")).Run(opt);
        Assert.Equal(expected, opt.ToString());
    }
}