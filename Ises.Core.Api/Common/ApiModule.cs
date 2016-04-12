using System.Web.Http.ExceptionHandling;
using Autofac;
using Ises.Core.Api.Exception;

namespace Ises.Core.Api.Common
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApiExceptionHandler>().As<IExceptionHandler>().InstancePerLifetimeScope();
        }
    }
}
