using System.Configuration;
using System.Web.Http;

namespace SSOApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        private static string publicServerIpToSwagger = ConfigurationManager.AppSettings["PublicServerIpToSwagger"];
        /// <summary>
        /// 
        /// </summary>
        public static string URL = publicServerIpToSwagger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            //Habilitación de CORS [NuGet instalado para esta operación]
            config.EnableCors();
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "SSO-API",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
