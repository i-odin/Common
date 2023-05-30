using Common.Core.QueryBuilders.Translators;
using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Translators;

public class PgTableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest public.TestClass")]
    public void Table_Build(string expected)
    {
        var source = new QueryBuilderSource();
        PgTableTranslator<TestClass>.Make("test", source, null).Run();
        Assert.Equal(expected, source.ToString());
    }
}
