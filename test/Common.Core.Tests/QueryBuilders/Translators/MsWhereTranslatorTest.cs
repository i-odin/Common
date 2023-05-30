using Common.Core.QueryBuilders.Queris;
using Common.Core.QueryBuilders.Translators;

namespace Common.Core.Tests.QueryBuilders.Translator;

public class MsWhereTranslatorTest
{
    [Theory]
    [InlineData("\r\nwhere Id = @0")]
    public void Where_BuildEqualTo(string expected)
    {
        var source = new QueryBuilderSource();
        MsWhereTranslator<TestClass>.Make(source, x => x.EqualTo(y => y.Id, Guid.NewGuid()));
        Assert.Equal(expected, source.ToString());
    }

    [Theory]
    [InlineData("\r\nwhere  and ")]
    public void Where_BuildAnd(string expected)
    {
        var source = new QueryBuilderSource();
        MsWhereTranslator<TestClass>.Make(source, x => x.And());
        Assert.Equal(expected, source.ToString());
    }
}
