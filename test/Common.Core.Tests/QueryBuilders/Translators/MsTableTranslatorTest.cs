using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.Tests.QueryBuilders.Translators;

public class MsTableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest dbo.TestClass")]
    public void Table_Build(string expected)
    {
        var source = new QueryBuilderSource();
        MsTableTranslator<TestClass>.Make("test", source,  null).Run();
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\ntest test.Test as test")]
    public void Table_BuildTableAndSchemaAndAlias(string expected)
    {
        var source = new QueryBuilderSource();
        MsTableTranslator<TestClass>.Make("test", source, x => x.WithTable("Test").WithSchema("test").WithAlias("test")).Run();
        Assert.Equal(expected, source.ToString());
    }
}