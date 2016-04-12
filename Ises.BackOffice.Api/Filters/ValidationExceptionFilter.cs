using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using FluentValidation;
using Ises.Core.Common;

namespace Ises.BackOffice.Api.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                var errorDetails = new Dictionary<string, string>();
                const string errorMessage = "Validation failed";

                var validationException = context.Exception as ValidationException;
                validationException.Errors.ToList().ForEach(validationFailure => errorDetails.Add(validationFailure.PropertyName, validationFailure.ErrorMessage));
                
                var apiResult = new ApiResult(MessageType.Danger)
                {
                    ApiError = new ApiError { Message = errorMessage, ErrorDetails = errorDetails }
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden, apiResult);
            }
        }
    }
}
