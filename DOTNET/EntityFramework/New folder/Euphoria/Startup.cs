//using Euphoria.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Techlabs.Euphoria.API;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Web.Common.OwinHost;
using Ninject;
using System.Reflection;
using Techlabs.Euphoria.Kernel.Framework;
using Techlabs.Euphoria.Kernel.Framework.ObjectResolvers.Ninject;
using Techlabs.Euphoria.API.Filters;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(Euphoria.Startup))]
namespace Euphoria
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.Filters.Add(new GlobalExceptionHandler());

            //OWIN Configuration
            ConfigureOAuth(app);

            //CORS Configuration
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Swagger Integration
            Swashbuckle.Bootstrapper.Init(config);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            //app.UseWebApi(config);
        }

        private static IKernel CreateKernel()
        {
            var kernel = ((NinjectObjectResolver)ObjectLocator.Instance.ObjectResolver).Kernel;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            return kernel; 
        }
        
        public void ConfigureOAuth(IAppBuilder app)
        {
           
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
               // Provider = new EuphoriaAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            // Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}