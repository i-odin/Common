using Common.Core.Models;

namespace Common.Core.Helpers
{
    //TODO: Тест
    public static class ErrorHelper
    {
        public static class System
        {
            public static Error InternalServer() =>
                new Error(СodeHelper.InternalServer, MessageHelper.InternalServer);
            public static Error InternalServerInnerException() =>
                new Error(СodeHelper.InternalServerInnerException, MessageHelper.InternalServerInnerException);
        }
    }
}
