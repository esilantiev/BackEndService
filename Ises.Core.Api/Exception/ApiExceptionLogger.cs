using System.Web.Http.ExceptionHandling;
using Ises.Core.Common;

namespace Ises.Core.Api.Exception
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            ApplicationContext.Logger.Error("Got Exception: ", context.Exception);
        }
    }
}
