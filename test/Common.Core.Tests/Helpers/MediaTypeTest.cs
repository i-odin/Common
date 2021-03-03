using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Tests.Helpers
{
    public class MediaTypeTest
    {
        [Theory]
        [InlineData(MediaType.ApplicationJson, "application/json")]
        public void MediaType_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
