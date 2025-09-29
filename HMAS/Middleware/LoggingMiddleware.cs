using System.Text.Json;
using System.Threading.Tasks;

namespace HMAS.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next,ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var reqLog = new
            {
                method = context.Request.Method,
                path = context.Request.Path,
                dateTime = DateTime.Now,
            };

            await LogToFileAsync("Request", reqLog);

            //_logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path} {DateTime.Now}");
            await _next(context);
            var resLog = new
            {
                status = context.Response.StatusCode
            };

            await LogToFileAsync("Response", resLog);
            //_logger.LogInformation($"Outgoing response: {context.Response.StatusCode}");
        }
        public async Task LogToFileAsync(string logType,object logData)
        {
            var logMess = $"{logType}: {JsonSerializer.Serialize(logData)}";
            await File.AppendAllTextAsync("logs.txt", logMess);
        }
    }
}
