namespace CodeProject.StoreDB.Portal
{
    using Microsoft.Owin.Security.OAuth;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="WebApiConfig" />
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// The Register
        /// </summary>
        /// <param name="config">The config<see cref="HttpConfiguration"/></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // New code
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
