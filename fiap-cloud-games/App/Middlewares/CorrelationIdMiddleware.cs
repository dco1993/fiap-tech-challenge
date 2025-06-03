using Domain;
using Microsoft.Extensions.Primitives;

namespace App.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _correlationIdHeader = "x-correlation-id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, CorrelationId correlationId)
        {
            var correlationIdString = GetCorrelationId(httpContext, correlationId);
            AddCorrelationIdHeaderToResponse(httpContext, correlationIdString);

            return _next(httpContext);
        }

        private static StringValues GetCorrelationId(HttpContext context, CorrelationId correlationId)
        {
            if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationIdStr))
            {
                correlationId.Set(correlationIdStr);
                return correlationIdStr;
            }
            else
            {
                correlationIdStr = Guid.NewGuid().ToString();
                correlationId.Set(correlationIdStr);
                return correlationIdStr;
            }
        }

        private static void AddCorrelationIdHeaderToResponse(HttpContext context, StringValues correlationId)
        => context.Response.OnStarting(() =>
        {
            context.Response.Headers[_correlationIdHeader] = new[] { correlationId.ToString() };
            return Task.CompletedTask;
        });
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
