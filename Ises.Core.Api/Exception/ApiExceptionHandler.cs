using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Ises.Core.Common;

namespace Ises.Core.Api.Exception
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var ex = context.Exception as ArgumentException;
            var content = "Internal Server Error. Please contact api administrator";
            var reason = ExceptionReason.GeneralException;
            if (ex != null)
            {
                content = ex.Message;
                reason = ExceptionReason.ArgumentException;
            }
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = content,
                Reason = reason
            };
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { private get; set; }
            public string Content { private get; set; }
            public ExceptionReason Reason { private get; set; }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(Content),
                    RequestMessage = Request,
                    ReasonPhrase = Reason.ToString(),
                    StatusCode = HttpStatusCode.InternalServerError,

                };
                return Task.FromResult(response);
            }
        }
    }
}
