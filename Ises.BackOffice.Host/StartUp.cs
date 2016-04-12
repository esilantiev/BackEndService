using Autofac;
using Ises.Application.Mappers;
using Ises.BackOffice.Api;
using Ises.Core.Common;
using Ises.Core.Common.Middleware;
using Ises.Core.Hosting;
using Microsoft.Owin.Cors;
using Owin;

namespace Ises.BackOffice.Host
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfiguration.Configure();

            ApplicationContext.HostName = "ises";

            app.UseRequestLogger();

            app.UseExceptionHandler();

            app.UseCors(CorsOptions.AllowAll);

            var container = LoadContainer();
            app.UseAutofacMiddleware(container);

            var config = new ApiConfiguration(container);
            app.UseWebApi(config);
            app.UseAutofacWebApi(config);
        }

        static IContainer LoadContainer()
        {
            var builder = new ContainerBuilder();

            HostHelper.RegisterModules(builder);

            var container = builder.Build();

            return container;
        }
    }
}
