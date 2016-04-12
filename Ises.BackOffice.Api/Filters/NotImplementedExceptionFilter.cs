using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Ises.BackOffice.Api.Filters
{
    public class NotImplementedExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "test");
            }
        }
    }
}
