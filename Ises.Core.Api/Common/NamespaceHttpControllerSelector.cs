﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Ises.Core.Api.Common
{
    public class NamespaceHttpControllerSelector : DefaultHttpControllerSelector
    {
        const string ControllerKey = "controller";
        const string NamespaceKey = "Namespaces";
        readonly HttpConfiguration configuration;
        readonly Lazy<IEnumerable<Type>> duplicateControllerTypes;

        public NamespaceHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            this.configuration = configuration;
            duplicateControllerTypes = new Lazy<IEnumerable<Type>>(GetDuplicateControllerTypes);
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            if (routeData == null || routeData.Route == null || routeData.Route.DataTokens == null || !routeData.Route.DataTokens.ContainsKey(NamespaceKey) || routeData.Route.DataTokens[NamespaceKey] == null)
                return base.SelectController(request);

            // Look up controller in route data
            object controllerName;
            routeData.Values.TryGetValue(ControllerKey, out controllerName);
            var controllerNameAsString = controllerName as string;
            if (controllerNameAsString == null)
                return base.SelectController(request);

            //get the currently cached default controllers - this will not contain duplicate controllers found so if
            // this controller is found in the underlying cache we don't need to do anything
            var map = GetControllerMapping();
            if (map.ContainsKey(controllerNameAsString))
                return base.SelectController(request);

            //the cache does not contain this controller because it's most likely a duplicate, 
            // so we need to sort this out ourselves and we can only do that if the namespace token
            // is formatted correctly.
            var namespaces = routeData.Route.DataTokens[NamespaceKey] as IEnumerable<string>;
            if (namespaces == null)
                return base.SelectController(request);

            //see if this is in our cache
            var found = duplicateControllerTypes.Value
                                                .Where(x => string.Equals(x.Name, controllerNameAsString + ControllerSuffix, StringComparison.OrdinalIgnoreCase))
                                                .FirstOrDefault(x => namespaces.Contains(x.Namespace));

            if (found == null)
                return base.SelectController(request);

            return new HttpControllerDescriptor(configuration, controllerNameAsString, found);
        }

        IEnumerable<Type> GetDuplicateControllerTypes()
        {
            var assembliesResolver = configuration.Services.GetAssembliesResolver();
            var controllersResolver = configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            //we have all controller types, so just store the ones with duplicate class names - we don't
            // want to cache too much and the underlying selector caches everything else

            var duplicates = controllerTypes.GroupBy(x => x.Name)
                                            .Where(x => x.Count() > 1)
                                            .SelectMany(x => x)
                                            .ToArray();

            return duplicates;
        }
    }
}
