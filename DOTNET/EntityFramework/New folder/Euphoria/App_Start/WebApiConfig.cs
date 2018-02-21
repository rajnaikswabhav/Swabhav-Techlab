using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Techlabs.Euphoria.API.Filters;
using Techlabs.Euphoria.Kernel.Framework;
using Techlabs.Euphoria.Kernel.Framework.ObjectResolvers.Ninject;

namespace Techlabs.Euphoria.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
          //  var ninjectKernal = ((NinjectObjectResolver)ObjectLocator.Instance.ObjectResolver).Kernel;
          //  config.DependencyResolver = new NinjectDependencyResolver(ninjectKernal);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }

            //);

            GlobalConfiguration.Configuration.Filters.Add(new GlobalExceptionHandler());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
