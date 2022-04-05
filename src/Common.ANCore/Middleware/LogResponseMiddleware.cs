using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using Common.Core.Helpers;
using Common.Core.Utilities;
using Common.Core.Extensions;
using Common.Core.Structs;

namespace Common.ANCore.Middleware;

//TODO: Test
public class LogResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogResponseMiddleware> _logger;
    private readonly LogLevel _logLevel;

    public LogResponseMiddleware([NotNull] RequestDelegate next, [NotNull] ILoggerFactory loggerFactory)
    {
        Throw.NotNull(next);
        Throw.NotNull(loggerFactory);

        _next = next;
        _logger = loggerFactory.CreateLogger<LogResponseMiddleware>();
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

    private static string MessageBuild(HttpContext context, string body)
    {
        var spanValues = new ReadOnlySpan<KeyValueString>(new[]
        {
            new KeyValueString(Messages.TraceIdentifier, context.TraceIdentifier),
            new KeyValueString(Messages.Body, body)
        });
        return new StringBuilder(body.Length).AppendJoin(in spanValues).ToString();
    }
}