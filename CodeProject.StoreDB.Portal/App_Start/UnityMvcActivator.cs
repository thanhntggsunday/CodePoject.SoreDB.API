[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CodeProject.StoreDB.Portal.UnityMvcActivator), nameof(CodeProject.StoreDB.Portal.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(CodeProject.StoreDB.Portal.UnityMvcActivator), nameof(CodeProject.StoreDB.Portal.UnityMvcActivator.Shutdown))]
namespace CodeProject.StoreDB.Portal
{
    using System.Linq;
    using System.Web.Mvc;
    using Unity.AspNet.Mvc;

    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start()
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}
