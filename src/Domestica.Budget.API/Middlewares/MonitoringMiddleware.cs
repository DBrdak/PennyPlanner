using Serilog.Context;
using System.Diagnostics;
using Domestica.Budget.API.Logging;
using Serilog;
using MediatR;

namespace Domestica.Budget.API.Middlewares
{
    public sealed class MonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MonitoringMiddleware> _logger;
        private const int longRunningRequestThreshold = 750;

        public MonitoringMiddleware(RequestDelegate next, ILogger<MonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            using (LogContext.PushProperty("RequestId", context.TraceIdentifier))
            {
                var request = context.Request;
                var stopwatch = Stopwatch.StartNew();

                await _next(context);

                stopwatch.Stop();

                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                LogRequestExecutionTime(request, elapsedMilliseconds);
            }
        }

        private void LogRequestExecutionTime(HttpRequest request, long elapsedMilliseconds)
        {
            using (LogContext.PushProperty("LogType", LogTypes.RequestExecutionTimeLog))
            {
                switch (elapsedMilliseconds > longRunningRequestThreshold)
                {
                    case true:
                        var warningMessage =
                            $"Long running request: {request.Method} {request.Path} ({elapsedMilliseconds} milliseconds)";
                        _logger.LogWarning(warningMessage);
                        break;
                    case false:
                        var infoMessage = $"Request: {request.Method} {request.Path} ({elapsedMilliseconds} milliseconds)";
                        _logger.LogInformation(infoMessage);
                        break;
                }
            }
        }
    }
}
