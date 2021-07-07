using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils
{
    public class OuterApiResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public string GenericStatus { get; set; }

        public OuterApiResponse SetGenericStatus(string genericStatus)
        {
            GenericStatus = genericStatus;
            return this;
        }

        public OuterApiResponse SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public bool IsSuccessResponse()
        {
            var status = (int)Status;
            return (status >= 1 && status <= 3);
        }

        public bool IsFailResponse()
        {
            var status = (int)Status;
            return (status >= 4 && status <= 7);
        }

        public bool IsErrorResponse()
        {
            return Status == ResponseStatus.Error;
        }

        public static OuterApiResponse Result(ResponseStatus status, string message = null)
        {
            return new OuterApiResponse
            {
                Status = status,
                Message = message
            };
        }

        public static OuterApiResponse SuccessResponse(string message = null)
        {
            return Result(ResponseStatus.Success, message);
        }

        public static OuterApiResponse SuccessCreatedResponse(string message = null)
        {
            return Result(ResponseStatus.SuccessCreated, message);
        }

        public static OuterApiResponse SuccessNoContentResponse(string message = null)
        {
            return Result(ResponseStatus.SuccessNoContent, message);
        }

        public static OuterApiResponse FailResponse(string message = null)
        {
            return Result(ResponseStatus.Fail, message);
        }

        public static OuterApiResponse NotFoundResponse(string message = null)
        {
            return Result(ResponseStatus.NotFound, message);
        }

        public static OuterApiResponse UnAuthorizedResponse(string message = null)
        {
            return Result(ResponseStatus.Unauthorized, message);
        }

        public static OuterApiResponse AccessDeniedResponse(string message = null)
        {
            return Result(ResponseStatus.AccessDenied, message);
        }

        public static OuterApiResponse ErrorResponse(string message = null)
        {
            return Result(ResponseStatus.Error, message);
        }
    }
}
