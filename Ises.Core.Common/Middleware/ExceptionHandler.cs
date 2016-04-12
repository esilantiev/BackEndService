using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Ises.Core.Common.Middleware
{
    internal class ExceptionHandler : OwinMiddleware
    {
        public ExceptionHandler(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (AggregateException ex)
            {
                ApplicationContext.Logger.Error("Aggregate Exception ", ex);
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("General Exception ", ex);
            }
        }
    }
}
