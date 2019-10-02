namespace CodeProject.StoreDB.Portal
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessService;
    using CodeProject.StoreDB.BusinessSevice.BusinessServiceModels;
    using CodeProject.StoreDB.DataService.UnitOfWork;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Portal.Controllers;
    using System;
    using Unity;
    using Unity.Injection;

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Defines the container
        /// </summary>
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Gets the Container
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IShoppingCartBusinessService, ShoppingCartBusinessService>();

            container.RegisterType<IOrderBusinessService, OderBusinessService>();
            container.RegisterType<IOrderDetailBusinessService, OrderDetailBusinessService>();
            container.RegisterType<IPostBusinessService, PostBusinessService>();
            container.RegisterType<IPostCategoryBusinessService, PostCategoryBusinessService>();
            container.RegisterType<IProductBusinessService, ProductBusinessService>();
            container.RegisterType<IProductCategoryBusinessService, ProductCategoryBusinessService>();
            container.RegisterType<ITagBusinessService, TagBusinessService>();
        }
    }
}
