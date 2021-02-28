using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class MediaTypeHelperTests
    {
        [Theory]
        [InlineData(MediaTypeHelper.ApplicationJson, "application/json")]
        public void MediaType_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }
    }
}
