using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Tests.Helpers
{
    public class MessageTest
    {
        [Theory]
        [InlineData(Messages.Body, "Body")]
        [InlineData(Messages.Method, "Method")]
        [InlineData(Messages.TraceIdentifier, "Trace Identifier")]
        [InlineData(Messages.Url, "Url")]
        public void Messages_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
