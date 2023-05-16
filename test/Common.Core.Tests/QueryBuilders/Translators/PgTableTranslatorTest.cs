using Common.Core.QueryBuilders.Translators;
using Common.Core.QueryBuilders;

namespace Common.Core.Tests.QueryBuilders.Translators;

public class PgTableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest public.TestClass")]
    public void Table_Build(string expected)
    {
        var opt = new QueryBuilderOptions();
        PgTableTranslator<TestClass>.Make("test", null).Run(opt);
        Assert.Equal(expected, opt.ToString());
    }
}
