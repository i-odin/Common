using Common.Core.Helpers;
using Common.Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Models;
using Common.Core.Utilities;

namespace Common.AspNetCore.Middleware
{
    //TODO: Тест
    public class LogErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogErrorMiddleware> _logger;
        private readonly ISerializerWrapper _serializer;

        public LogErrorMiddleware([NotNull] RequestDelegate next, [NotNull] ILoggerFactory loggerFactory)
        {
            Throw.NotNull(next, nameof(next));
            Throw.NotNull(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<LogErrorMiddleware>();
            _serializer = new JsonTextSerializerWrapper();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, Errors.Message.InternalServer);
            if (exception is AggregateException aggregateException && aggregateException.InnerExceptions.Count > 0)
                foreach (Exception innerException in aggregateException.InnerExceptions)
                    _logger.LogError(innerException, Errors.Message.InternalServerInnerException);

            var error = new Error(nameof(HttpStatusCode.InternalServerError), exception.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(_serializer.Serialize(error));
        }
    }
}
