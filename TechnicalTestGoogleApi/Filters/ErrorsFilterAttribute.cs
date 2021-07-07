using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Filters
{
    public class ErrorsFilterAttribute: ExceptionFilterAttribute
    {
        public ErrorsFilterAttribute()
        {

        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";

            context.HttpContext.Response.WriteAsync(new ApiResponse((int) HttpStatusCode.InternalServerError).ToString());

            return base.OnExceptionAsync(context);
        }
    }
}
