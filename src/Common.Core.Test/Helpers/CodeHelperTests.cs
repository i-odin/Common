using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class CodeHelperTests
    {
        [Theory]
        [InlineData(CodeHelper.InternalServer, "system.internal.server")]
        [InlineData(CodeHelper.InternalServerInnerException, "system.internal.server.inner.exception")]
        public void CodeHelper_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
