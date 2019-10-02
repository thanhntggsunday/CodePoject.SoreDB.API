namespace CodeProject.Business.Common.Automapper
{
    using AutoMapper;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;

    /// <summary>
    /// Defines the <see cref="AutoMapperConfig" />
    /// </summary>
    public class AutoMapperConfig
    {
        // public static IMapper Mapper = new Mapper();
        /// <summary>
        /// The ConfigureAutoMapper
        /// </summary>
        public static void ConfigureAutoMapper()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(p => p.ID, opt => opt.MapFrom(pm => pm.ID))
                    .ForMember(x => x.CategoryName, opt => opt.Ignore());

                cfg.CreateMap<ProductCategory, ProductCategoryDTO>()
                  .ForMember(p => p.ID, opt => opt.MapFrom(pm => pm.ID))
                  .ForMember(p => p.Name, opt => opt.MapFrom(pm => pm.Name));

                cfg.CreateMap<PostCategory, PostCategoryDTO>()
                 .ForMember(p => p.ID, opt => opt.MapFrom(pm => pm.ID))
                 .ForMember(p => p.Name, opt => opt.MapFrom(pm => pm.Name));

                cfg.CreateMap<Post, PostDTO>()
                   .ForMember(p => p.ID, opt => opt.MapFrom(pm => pm.ID))
                   .ForMember(x => x.CategoryName, opt => opt.Ignore());

                cfg.CreateMap<Order, OrderDTO>()
                  .ForMember(p => p.ID, opt => opt.MapFrom(pm => pm.ID));

                cfg.CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(p => p.OrderID, opt => opt.MapFrom(pm => pm.OrderID))
                .ForMember(p => p.ProductID, opt => opt.MapFrom(pm => pm.ProductID))
                .ForMember(p => p.ProductName, opt => opt.Ignore());

            });
#pragma warning restore CS0618 // Type or member is obsolete

            Mapper.AssertConfigurationIsValid();
        }
    }
}
