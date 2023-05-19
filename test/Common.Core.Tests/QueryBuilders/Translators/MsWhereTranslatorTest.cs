using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.Tests.QueryBuilders.Translator;

public class MsWhereTranslatorTest
{
    [Theory]
    [InlineData("\r\nwhere Id = @Id0")]
    public void Where_BuildEqualTo(string expected)
    {
        var source = new QueryBuilderSource();
        MsWhereTranslator<TestClass>.Make(x => x.EqualTo(y => y.Id, Guid.NewGuid())).Run(source);
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\nwhere  and ")]
    public void Where_BuildAnd(string expected)
    {
        var source = new QueryBuilderSource();
        MsWhereTranslator<TestClass>.Make(x => x.And()).Run(source);
        Assert.Equal(expected, source.ToString());
    }
}
