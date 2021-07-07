using Data.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TechnicalTestGoogleApi.Utils
{
    public class ApiResponse
    {
        public int Code { get; set; }

        public string Status { get; set; } = "unknown";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        public ApiResponse(int statusCode = 200)
        {
            if (!Enum.IsDefined(typeof(HttpStatusCode), statusCode))
                throw new Exception("Tipo de codigo de respuesta invalido");

            Status = GetStatusName(statusCode);
            Code = statusCode;
            if (Status == "error")
                Message = GetStatusCodeMessage(statusCode);
        }

        public ApiResponse SetData(object data)
        {
            Data = data;
            return this;
        }

        public static string GetStatusCodeMessage(int statusCode)
        {
            string message = Enum.GetName(typeof(HttpStatusCode), statusCode);
            string[] split = System.Text.RegularExpressions.Regex.Split(message, @"(?<!^)(?=[A-Z])");
            return string.Join(" ", split);
        }

        public static string GetStatusName(int statusCode)
        {
            if (statusCode >= 100 && statusCode <= 299)
                return "success";

            if (statusCode >= 300 && statusCode <= 499)
                return "fail";

            if (statusCode >= 500 && statusCode <= 599)
                return "error";

            return "unknown";
        }

        public HttpStatusCode GetStatusCode()
        {
            return (HttpStatusCode)Code;
        }

        public static Dictionary<string, string[]> GetMessageList(ModelStateDictionary modelState)
        {
            var errorList = new Dictionary<string, string[]>();
            foreach (var (key, value) in modelState)
            {
                var errors = value.Errors;
                if (errors == null || errors.Count <= 0)
                    continue;

                var errorMessages = new string[errors.Count];
                for (var i = 0; i < errors.Count; i++)
                {
                    var errorMessage = string.IsNullOrEmpty(errors[i].ErrorMessage) ? "La entrada no fue válida." : errors[i].ErrorMessage;
                    errorMessages[i] = errorMessage;
                }

                errorList.Add(key, errorMessages);
            }

            return errorList;
        }

        /// <summary>
        /// Devuelve un objeto ActionResult con contenido en formato Json
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ActionResult Result(int statusCode, object data = null, string message = null)
        {
            var response = new ApiResponse(statusCode);

            if (data != null)
                response.Data = data;

            if (message != null)
                response.Message = message;

            return response;
        }

        /// <inheritdoc cref="HttpStatusCode.OK"/>
        public static ActionResult Ok(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.OK, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.OK"/>
        public static ActionResult Ok(object data)
        {
            return Ok(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.OK"/>
        public static ActionResult Ok(string message)
        {
            return Ok(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Created"/>
        public static ActionResult Created(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.Created, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Created"/>
        public static ActionResult Created(object data)
        {
            return Created(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.Created"/>
        public static ActionResult Created(string message)
        {
            return Created(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Accepted"/>
        public static ActionResult Accepted(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.Accepted, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Accepted"/>
        public static ActionResult Accepted(object data)
        {
            return Accepted(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.Accepted"/>
        public static ActionResult Accepted(string message)
        {
            return Accepted(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.BadRequest"/>
        public static ActionResult BadRequest(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.BadRequest, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.BadRequest"/>
        public static ActionResult BadRequest(object data)
        {
            return BadRequest(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.BadRequest"/>
        public static ActionResult BadRequest(string message)
        {
            return BadRequest(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Unauthorized"/>
        public static ActionResult Unauthorized(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.Unauthorized, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Unauthorized"/>
        public static ActionResult Unauthorized(object data)
        {
            return Unauthorized(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.Unauthorized"/>
        public static ActionResult Unauthorized(string message)
        {
            return Unauthorized(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Forbidden"/>
        public static ActionResult Forbidden(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.Forbidden, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.Forbidden"/>
        public static ActionResult Forbidden(object data)
        {
            return Forbidden(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.Forbidden"/>
        public static ActionResult Forbidden(string message)
        {
            return Forbidden(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.NotFound"/>
        public static ActionResult NotFound(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.NotFound, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.NotFound"/>
        public static ActionResult NotFound(object data)
        {
            return NotFound(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.NotFound"/>
        public static ActionResult NotFound(string message)
        {
            return NotFound(null, message);
        }

        /// <inheritdoc cref="HttpStatusCode.InternalServerError"/>
        public static ActionResult Error(object data = null, string message = null)
        {
            return Result((int)HttpStatusCode.InternalServerError, data, message);
        }

        /// <inheritdoc cref="HttpStatusCode.InternalServerError"/>
        public static ActionResult Error(object data)
        {
            return Error(data, null);
        }

        /// <inheritdoc cref="HttpStatusCode.InternalServerError"/>
        public static ActionResult Error(string message)
        {
            return Error(null, message);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static implicit operator ActionResult(ApiResponse response)
        {
            return new ContentResult
            {
                Content = response.ToString(),
                StatusCode = (int)response.GetStatusCode(),
                ContentType = "application/json"
            };
        }

        public static explicit operator ApiResponse(OuterApiResponse response)
        {
            var statusCode = HttpStatusCode.OK;
            switch (response.Status)
            {
                case ResponseStatus.SuccessCreated:
                    statusCode = HttpStatusCode.Created;
                    break;
                case ResponseStatus.SuccessNoContent:
                    statusCode = HttpStatusCode.NoContent;
                    break;
                case ResponseStatus.Fail:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ResponseStatus.Unauthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ResponseStatus.AccessDenied:
                    statusCode = HttpStatusCode.Forbidden;
                    break;
                case ResponseStatus.NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case ResponseStatus.Error:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            return new ApiResponse((int)statusCode)
            {
                Message = response.Message
            };
        }
    }
}
