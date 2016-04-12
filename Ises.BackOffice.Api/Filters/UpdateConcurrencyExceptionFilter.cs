using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Ises.Core.Common;
using Ises.Domain.Areas;
using RefactorThis.GraphDiff.CustomExceptions;

namespace Ises.BackOffice.Api.Filters
{
    public class UpdateConcurrencyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DbUpdateConcurrencyException<Area>)
            {
                var exception = context.Exception as DbUpdateConcurrencyException<Area>;

                var errorDetails = new Dictionary<string, string>();
                const string errorMessage = "The record you attempted to edit "
                                            + "was modified by another user after you got the original value. The "
                                            + "edit operation was canceled and the current values in the database "
                                            + "have been displayed. If you still want to edit this record, click "
                                            + "the Save button again.";

                if (exception.DatabaseEntity.Name != exception.ClientEntity.Name) errorDetails.Add("Name", "Current value: " + exception.DatabaseEntity.Name);

                var apiResult = new ApiResult(MessageType.Warning)
                {
                    AdditionalDetails = new Dictionary<object, object> { { "rowVersion", exception.DatabaseEntity.RowVersion } },
                    ApiError = new ApiError { Message = errorMessage, ErrorDetails = errorDetails}
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden, apiResult);
            }
        }
    }
}
