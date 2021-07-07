using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Middlewares
{
    public class ClientUnauthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientUnauthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var statusCode = context.Response.StatusCode;
            string contentType = "application/json";
            if (statusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = contentType;
                await context.Response.WriteAsync(new ApiResponse(statusCode)
                {
                    Message = "La sesión actual no es válida."
                }.ToString()) ;
            }
            else if(statusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.ContentType = contentType;
                await context.Response.WriteAsync(new ApiResponse(statusCode)
                {
                    Message = "Acceso denegado."
                }.ToString());
            }
        }
    }

    public static class ClientUnauthorizeMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientUnauthorizeMiddleware(this IApplicationBuilder app) {
            return app.UseMiddleware<ClientUnauthorizeMiddleware>();
        }
    }
}
