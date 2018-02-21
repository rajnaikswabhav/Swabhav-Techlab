using Swashbuckle.Application;
using System.Web.Http;
using Techlabs.Euphoria.API;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Techlabs.Euphoria.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);

            // NOTE: If you want to customize the generated swagger or UI, use SwaggerSpecConfig and/or SwaggerUiConfig here ...

            SwaggerSpecConfig.Customize(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\WebapiSwaggerDoc.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
    
}