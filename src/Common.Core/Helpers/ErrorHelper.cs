using Common.Core.Models;

namespace Common.Core.Helpers
{
    //TODO: Тест
    public static class ErrorHelper
    {
        public static class System
        {
            public static Error InternalServer() =>
                new Error(CodeHelper.InternalServer, MessageHelper.InternalServer);
            public static Error InternalServerInnerException() =>
                new Error(CodeHelper.InternalServerInnerException, MessageHelper.InternalServerInnerException);
        }
    }
}
