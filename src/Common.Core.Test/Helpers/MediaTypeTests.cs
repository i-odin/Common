using Common.Core.Helpers;
using Xunit;

namespace Common.Core.Test.Helpers
{
    public class MediaTypeTests
    {
        [Fact]
        public void MediaTypeApplicationJsonEqual()
        {
            Assert.Equal("application/json", MediaType.ApplicationJson);
        }
    }
}
