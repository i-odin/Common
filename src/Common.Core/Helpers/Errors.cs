using Common.Core.Models;

namespace Common.Core.Helpers;

public static class Errors
{
    public static class Message
    {
        public const string InternalServer = "Internal Server Error";
        public const string InternalServerInnerException = "Internal Server Error (Inner Exception)";
    }

    public static class Code
    {
        public const string InternalServer = "system.internal.server";
        public const string InternalServerInnerException = "system.internal.server.inner.exception";
    }

    public static class System
    {
        public static Error InternalServer() =>
            new(Code.InternalServer, Message.InternalServer);
        public static Error InternalServerInnerException() =>
            new(Code.InternalServerInnerException, Message.InternalServerInnerException);
    }
}
