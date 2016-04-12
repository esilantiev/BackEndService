using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace Ises.Core.Common.Middleware
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseRequestLogger(this IAppBuilder app)
        {
            return app.Use<RequestLogger>();
        }
        public static IAppBuilder UseExceptionHandler(this IAppBuilder app)
        {
            return app.Use<ExceptionHandler>();
        }
    }
}
