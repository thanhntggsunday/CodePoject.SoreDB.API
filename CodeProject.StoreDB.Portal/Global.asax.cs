﻿using CodeProject.Business.Common.Automapper;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeProject.StoreDB.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.ConfigureAutoMapper();
        }
    }
}