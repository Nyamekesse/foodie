using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Middlewares
{
    public class RequestsTimeLoggingMiddleware(ILogger<RequestsTimeLoggingMiddleware> _logger)
        : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopWatch.Stop();

            if (stopWatch.ElapsedMilliseconds / 1000 > 4)
            {
                _logger.LogInformation(
                    $"Request {context.Request.Method} {context.Request.Path} took {stopWatch.ElapsedMilliseconds} ms"
                );
            }
        }
    }
}
