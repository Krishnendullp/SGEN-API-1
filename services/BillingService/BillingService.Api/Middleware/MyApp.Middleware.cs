using System.Diagnostics;

namespace MyApp.Middleware
{
    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTrackingMiddleware> _logger;

        public RequestTrackingMiddleware(RequestDelegate next, ILogger<RequestTrackingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            _logger.LogInformation("➡️ Request: {method} {url}", request.Method, request.Path);

            try
            {
                await _next(context); // Pass to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Unhandled exception during request: {method} {url}", request.Method, request.Path);
                context.Response.StatusCode = 500;

                // Optional: return JSON error response
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal Server Error",
                    message = ex.Message
                });
                return;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("✅ Response: {statusCode} for {method} {url} in {elapsed}ms",
                    context.Response.StatusCode,
                    request.Method,
                    request.Path,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }

    // Extension method to easily add it to the pipeline
    public static class RequestTrackingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTracking(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTrackingMiddleware>();
        }
    }
}