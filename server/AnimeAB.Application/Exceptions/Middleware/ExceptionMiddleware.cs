
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnimeAB.Application.Common.ExceptionsHanlder.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var error500 = new Error
            {
                code = context.Response.StatusCode,
                message = "Internal Server Error",
                status = "INTERVAL_SERVER_ERROR"
            };

            var errorResponse = new ErrorResponse
            {
                error = error500
            };

            var json = JsonSerializer.Serialize(errorResponse.ToString(), options);

            await context.Response.WriteAsync(json);
        }
    }
}
