using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class MessageHelperTests
    {
        [Theory]
        [InlineData(MessageHelper.InternalServer, "Internal Server Error")]
        [InlineData(MessageHelper.InternalServerInnerException, "Internal Server Error (Inner Exception)")]
        public void MessageHelper_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
