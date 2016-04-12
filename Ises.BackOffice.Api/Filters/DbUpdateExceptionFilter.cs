using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Ises.Core.Common;
using Newtonsoft.Json;

namespace Ises.BackOffice.Api.Filters
{
    public class DbUpdateExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DbUpdateException)
            {
                var sqlException = (SqlException)context.Exception.GetBaseException();

                var sqlErrorCodes = JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(Configuration.ConfigFile.Path + Configuration.ConfigFile.SqlErrorCodesFile));
                const string errorMessage = "Operation failed";
                var errorDetails = new Dictionary<string, string>();

                foreach (var error in sqlException.Errors)
                {
                    errorDetails.Add(((SqlError)error).Number.ToString(CultureInfo.InvariantCulture), sqlErrorCodes[((SqlError)error).Number]);
                }

                var apiResult = new ApiResult(MessageType.Danger)
                {
                    ApiError = new ApiError { Message = errorMessage, ErrorDetails = errorDetails }
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden, apiResult);
            }
        }
    }
}
