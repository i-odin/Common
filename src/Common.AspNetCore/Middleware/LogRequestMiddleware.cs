using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Utilities;

namespace Common.AspNetCore.Middleware
{
    //TODO: Test
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequestMiddleware> _logger;
        private readonly LogLevel _logLevel;
        public LogRequestMiddleware([NotNull] RequestDelegate next, [NotNull] ILoggerFactory loggerFactory)
        {
            Throw.NotNull(next, nameof(next));
            Throw.NotNull(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<LogRequestMiddleware>();
            _logLevel = LogLevel.Information;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_logger.IsEnabled(_logLevel))
            {
                Stream originalRequestBody = context.Request.Body;
                (MemoryStream requestBody, string body) = await ReadBody(context);
                _logger.Log(_logLevel, MessageBuild(context, body));
                context.Request.Body = requestBody;
                await _next(context);
                context.Request.Body = originalRequestBody;
            }
            else
            {
                await _next(context);
            }
        }

        //TODO: сделать строитель предложений? 
        private static string MessageBuild(HttpContext context, string body)
        {
            //TODO: Вынести
            var sb = new StringBuilder(body.Length);
            sb.Append("TRACE IDENTIFIER= ");
            sb.Append(context.TraceIdentifier);
            sb.Append(", ");
            sb.Append("URL= ");
            sb.Append(context.Request.Host.ToString());
            sb.Append(", ");
            sb.Append("METHOD= ");
            sb.Append(context.Request.Method);
            sb.Append(", ");
            sb.Append("BODY= ");
            sb.Append(body);
            return sb.ToString();
        }

        private static async Task<(MemoryStream, string)> ReadBody(HttpContext context)
        {
            var requestBody = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            string body = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);
            return (requestBody, body);
        }
    }
}
