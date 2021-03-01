using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class MediaTypeTests
    {
        [Theory]
        [InlineData(MediaType.ApplicationJson, "application/json")]
        public void MediaType_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
