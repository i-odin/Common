using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.AspNetCore.Middlewaries
{
    //TODO: Test
    public class LogResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogResponseMiddleware> _logger;
        private readonly LogLevel _logLevel;

        public LogResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogResponseMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory)); ;
            _logLevel = LogLevel.Information;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_logger.IsEnabled(_logLevel))
            {
                Stream originalResponseBody = context.Response.Body;
                var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;
                await _next(context);
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                _logger.Log(_logLevel, MessageBuild(context, await new StreamReader(responseBodyStream).ReadToEndAsync()));
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBody);
            }
            else
            {
                await _next(context);
            }
        }

        //TODO: сделать строитель предложений? 
        private static string MessageBuild(HttpContext context, string body)
        {
            var sb = new StringBuilder(body.Length);
            //TODO: Вынести
            sb.Append("TRACE IDENTIFIER= ");
            sb.Append(context.TraceIdentifier);
            sb.Append(", ");
            sb.Append("BODY= ");
            sb.Append(body);
            return sb.ToString();
        }
    }
}
