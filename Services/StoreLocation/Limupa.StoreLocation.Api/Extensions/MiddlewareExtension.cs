using Limupa.StoreLocation.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Limupa.StoreLocation.Api.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseBadwordHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JsonBodyMiddleware>()
                      .UseMiddleware<BadWordsHandlerMiddleware>()
                      .UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
