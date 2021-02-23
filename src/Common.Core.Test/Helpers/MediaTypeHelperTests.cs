using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class MediaTypeHelperTests
    {
        [Fact]
        public void MediaTypeApplicationJsonEqual()
        {
            Assert.Equal(expected: "application/json", actual: MediaTypeHelper.ApplicationJson);
        }
    }
}
