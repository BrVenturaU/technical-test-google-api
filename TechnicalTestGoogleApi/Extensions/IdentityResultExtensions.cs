using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Extensions
{
    public static class IdentityResultExtensions
    {
        public static (ActionResult response, bool isSucceeded)  GetIdentityErrors(this IdentityResult result)
        {
            var modelState = new ModelStateDictionary();
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error => modelState.TryAddModelError(error.Code, error.Description));

                return (ApiResponse.BadRequest(ApiResponse.GetMessageList(modelState)), !result.Succeeded);
            }
            return (ApiResponse.Ok(), result.Succeeded);
        }
    }
}
