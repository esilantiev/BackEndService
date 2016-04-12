using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using Ises.BackOffice.Api.Controllers;
using Ises.BackOffice.Api.Filters;
using Ises.Core.Api.Common;
using Ises.Core.Api.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ises.BackOffice.Api
{
    public class ApiConfiguration : HttpConfiguration
    {
        public ApiConfiguration(ILifetimeScope container)
        {
            this.MapHttpAttributeRoutes();
            ConfigureRoutes();
            ConfigureJsonSerialization();
            DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Services.Replace(typeof(IExceptionHandler), container.Resolve<IExceptionHandler>());
            Services.Replace(typeof(IExceptionLogger), new ApiExceptionLogger());
            Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(this));

            Formatters.Remove(Formatters.XmlFormatter);
            Filters.AddRange(GetGlobalFilters());
        }

        private void ConfigureRoutes()
        {
            var route = Routes.MapHttpRoute(
                name: "BackOfficeApi",
                routeTemplate: "api/backoffice/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                });
            route.DataTokens["Namespaces"] = new[] { typeof(SystemController).Namespace };
        }

        private void ConfigureJsonSerialization()
        {
            var jsonSettings = Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        }

        private static IEnumerable<IFilter> GetGlobalFilters()
        {
            var globalFilters = new List<IFilter>
            {
                new NotImplementedExceptionFilter(),
                new UpdateConcurrencyExceptionFilter(),
                new ValidationExceptionFilter(),
                new DbUpdateExceptionFilter()
            };
            return globalFilters;
        }
    }
}
