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
public class LogRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestMiddleware> _logger;
    private readonly LogLevel _logLevel;
    public LogRequestMiddleware([NotNull] RequestDelegate next, [NotNull] ILoggerFactory loggerFactory)
    {
        Throw.NotNull(next);
        Throw.NotNull(loggerFactory);

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

    private static string MessageBuild(HttpContext context, string body)
    {
        var spanValues = new ReadOnlySpan<KeyValueString>(new[]
        {
            new KeyValueString(Messages.TraceIdentifier, context.TraceIdentifier),
            new KeyValueString(Messages.Url, context.Request.Host.ToString()),
            new KeyValueString(Messages.Method, context.Request.Method), new KeyValueString(Messages.Body, body)
        });
        return new StringBuilder(body.Length).AppendJoin(in spanValues).ToString();
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