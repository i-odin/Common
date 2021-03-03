using Common.Core.Helpers;
using Common.Core.Models;
using Xunit;

namespace Common.Core.Tests.Helpers
{
    public class ErrorsTest
    {
        [Theory]
        [InlineData(Errors.Message.InternalServer, "Internal Server Error")]
        [InlineData(Errors.Message.InternalServerInnerException, "Internal Server Error (Inner Exception)")]
        public void Message_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }

        [Theory]
        [InlineData(Errors.Code.InternalServer, "system.internal.server")]
        [InlineData(Errors.Code.InternalServerInnerException, "system.internal.server.inner.exception")]
        public void Code_String_ReturnTrue(string input, string expected)
        {
            Assert.Equal(expected, input);
        }

        [Fact]
        public void InternalServer_Compare_ReturnTrue()
        {
            var internalServer = Errors.System.InternalServer();
            Assert.Equal(new Error(Errors.Code.InternalServer, Errors.Message.InternalServer), internalServer);
        }

        [Fact]
        public void InternalServerInnerException_Compare_ReturnTrue()
        {
            var internalServerInnerException = Errors.System.InternalServerInnerException();
            Assert.Equal(new Error(Errors.Code.InternalServerInnerException, Errors.Message.InternalServerInnerException), internalServerInnerException);
        }
    }
}
