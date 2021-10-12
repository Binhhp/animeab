using Microsoft.AspNetCore.Builder;

namespace AnimeAB.Application.Common.ExceptionsHanlder.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
