namespace CodeProject.StoreDB.Portal.Classes.Utilities
{
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;

    /// <summary>
    /// Defines the <see cref="Extensions" />
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The CopyFromProductCategoryDTO
        /// </summary>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        /// <param name="producCategorytDto">The producCategorytDto<see cref="ProductCategoryDTO"/></param>
        public static void CopyFromProductCategoryDTO(this ProductCategoryViewModel productCategoryViewModel, ProductCategoryDTO producCategorytDto)
        {
            productCategoryViewModel.ID = producCategorytDto.ID;
            productCategoryViewModel.Name = producCategorytDto.Name;
            productCategoryViewModel.Alias = producCategorytDto.Alias;
            productCategoryViewModel.Description = producCategorytDto.Description;
            productCategoryViewModel.Image = producCategorytDto.Image;
            productCategoryViewModel.MetaDescription = producCategorytDto.MetaDescription;
            productCategoryViewModel.MetaKeyword = producCategorytDto.MetaKeyword;
            productCategoryViewModel.Status = producCategorytDto.Status;
            productCategoryViewModel.HomeFlag = producCategorytDto.HomeFlag;
            productCategoryViewModel.UpdatedBy = producCategorytDto.UpdatedBy;
            productCategoryViewModel.UpdatedDate = producCategorytDto.UpdatedDate;
            productCategoryViewModel.CreatedBy = producCategorytDto.CreatedBy;
            productCategoryViewModel.UpdatedDate = producCategorytDto.UpdatedDate;
        }

        /// <summary>
        /// The CopyFromProductCategoryViewModel
        /// </summary>
        /// <param name="productCategory">The productCategory<see cref="ProductCategory"/></param>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        public static void CopyFromProductCategoryViewModel(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.Status = productCategoryViewModel.Status;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
        }

        /// <summary>
        /// The CopyFromProductDTO
        /// </summary>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        /// <param name="productDto">The productDto<see cref="ProductDTO"/></param>
        public static void CopyFromProductDTO(this ProductViewModel productViewModel, ProductDTO productDto)
        {
            productViewModel.Name = productDto.Name;
            productViewModel.Alias = productDto.Alias;
            productViewModel.Description = productDto.Description;
            productViewModel.Image = productDto.Image;
            productViewModel.MetaDescription = productDto.MetaDescription;
            productViewModel.MetaKeyword = productDto.MetaKeyword;
            productViewModel.Status = productDto.Status;
            productViewModel.HomeFlag = productDto.HomeFlag;
            productViewModel.CategoryID = productDto.CategoryID;
            productViewModel.Price = productDto.Price;
            productViewModel.PromotionPrice = productDto.PromotionPrice;
            productViewModel.Warranty = productDto.Warranty;
            productViewModel.Content = productDto.Content;
            productViewModel.MoreImages = productDto.MoreImages;
            productViewModel.ID = productDto.ID;
            productViewModel.CreatedBy = productDto.CreatedBy;
            productViewModel.CreatedDate = productDto.CreatedDate;
            productViewModel.UpdatedBy = productDto.UpdatedBy;
            productViewModel.UpdatedDate = productDto.UpdatedDate;
            productViewModel.CategoryName = productDto.CategoryName;
        }

        /// <summary>
        /// The CopyFromProductViewModel
        /// </summary>
        /// <param name="product">The product<see cref="Product"/></param>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        public static void CopyFromProductViewModel(this Product product, ProductViewModel productViewModel)
        {
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.Description = productViewModel.Description;
            product.Image = productViewModel.Image;
            product.MetaDescription = productViewModel.MetaDescription;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.Status = productViewModel.Status;
            product.HomeFlag = productViewModel.HomeFlag;
            product.CategoryID = productViewModel.CategoryID;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;
            product.Warranty = productViewModel.Warranty;
            product.Content = productViewModel.Content;
            product.MoreImages = productViewModel.MoreImages;
            product.ID = productViewModel.ID;
            product.CreatedBy = productViewModel.CreatedBy;
            product.CreatedDate = productViewModel.CreatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
        }

        /// <summary>
        /// The CopyFromPostCategoryDTO
        /// </summary>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        /// <param name="postCategoryDto">The postCategoryDto<see cref="PostCategoryDTO"/></param>
        public static void CopyFromPostCategoryDTO(this PostCategoryViewModel postCategoryViewModel, PostCategoryDTO postCategoryDto)
        {
            postCategoryViewModel.Name = postCategoryDto.Name;
            postCategoryViewModel.Alias = postCategoryDto.Alias;
            postCategoryViewModel.Description = postCategoryDto.Description;
            postCategoryViewModel.Image = postCategoryDto.Image;
            postCategoryViewModel.MetaDescription = postCategoryDto.MetaDescription;
            postCategoryViewModel.MetaKeyword = postCategoryDto.MetaKeyword;
            postCategoryViewModel.Status = postCategoryDto.Status;
            postCategoryViewModel.HomeFlag = postCategoryDto.HomeFlag;
            postCategoryViewModel.ID = postCategoryDto.ID;
            postCategoryViewModel.CreatedDate = postCategoryDto.CreatedDate;
            postCategoryViewModel.CreatedBy = postCategoryDto.CreatedBy;
            postCategoryViewModel.UpdatedDate = postCategoryDto.UpdatedDate;
            postCategoryViewModel.UpdatedBy = postCategoryDto.UpdatedBy;
        }

        /// <summary>
        /// The CopyFromPostCategoryViewModel
        /// </summary>
        /// <param name="postCategory">The postCategory<see cref="PostCategory"/></param>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        public static void CopyFromPostCategoryViewModel(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.Status = postCategoryViewModel.Status;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
        }

        /// <summary>
        /// The CopyFromPostDTO
        /// </summary>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        /// <param name="postDto">The postDto<see cref="PostDTO"/></param>
        public static void CopyFromPostDTO(this PostViewModel postViewModel, PostDTO postDto)
        {
            postViewModel.Name = postDto.Name;
            postViewModel.Alias = postDto.Alias;
            postViewModel.Description = postDto.Description;
            postViewModel.Image = postDto.Image;
            postViewModel.MetaDescription = postDto.MetaDescription;
            postViewModel.MetaKeyword = postDto.MetaKeyword;
            postViewModel.Status = postDto.Status;
            postViewModel.HomeFlag = postDto.HomeFlag;
            postViewModel.HotFlag = postDto.HotFlag;
            postViewModel.CategoryID = postDto.CategoryID;
            postViewModel.Content = postDto.Content;
            postViewModel.ID = postDto.ID;
            postViewModel.CreatedBy = postDto.CreatedBy;
            postViewModel.CreatedDate = postDto.CreatedDate;
            postViewModel.UpdatedBy = postDto.UpdatedBy;
            postViewModel.UpdatedDate = postDto.UpdatedDate;
            postViewModel.CategoryName = postDto.CategoryName;
        }

        /// <summary>
        /// The CopyFromPostViewModel
        /// </summary>
        /// <param name="post">The post<see cref="Post"/></param>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        public static void CopyFromPostViewModel(this Post post, PostViewModel postViewModel)
        {
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.Image = postViewModel.Image;
            post.MetaDescription = postViewModel.MetaDescription;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.Status = postViewModel.Status;
            post.HomeFlag = postViewModel.HomeFlag;
            post.HotFlag = postViewModel.HotFlag;
            post.CategoryID = postViewModel.CategoryID;
            post.Content = postViewModel.Content;
            post.ID = postViewModel.ID;
            post.CreatedBy = postViewModel.CreatedBy;
            post.CreatedDate = postViewModel.CreatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
        }

        /// <summary>
        /// The CopyFromOrderDTO
        /// </summary>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <param name="orderDto">The orderDto<see cref="OrderDTO"/></param>
        public static void CopyFromOrderDTO(this OrderViewModel orderViewModel, OrderDTO orderDto)
        {
            orderViewModel.CustomerAddress = orderDto.CustomerAddress;
            orderViewModel.CustomerEmail = orderDto.CustomerEmail;
            orderViewModel.CustomerMessage = orderDto.CustomerMessage;
            orderViewModel.CustomerMobile = orderDto.CustomerMobile;
            orderViewModel.CustomerName = orderDto.CustomerName;
            orderViewModel.CreatedBy = orderDto.CreatedBy;
            orderViewModel.CreatedDate = orderDto.CreatedDate;
            orderViewModel.ID = orderDto.ID;
            orderViewModel.PaymentMethod = orderDto.PaymentMethod;
            orderViewModel.PaymentStatus = orderDto.PaymentStatus;
            orderViewModel.Status = orderDto.Status;
            orderViewModel.TotalPrice = orderDto.Total;
        }

        /// <summary>
        /// The CopyFromOrderViewModel
        /// </summary>
        /// <param name="order">The order<see cref="Order"/></param>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        public static void CopyFromOrderViewModel(this Order order, OrderViewModel orderViewModel)
        {
            order.CustomerAddress = orderViewModel.CustomerAddress;
            order.CustomerEmail = orderViewModel.CustomerEmail;
            order.CustomerMessage = orderViewModel.CustomerMessage;
            order.CustomerMobile = orderViewModel.CustomerMobile;
            order.CustomerName = orderViewModel.CustomerName;
            order.CreatedBy = orderViewModel.CreatedBy;
            order.CreatedDate = orderViewModel.CreatedDate;
            order.ID = orderViewModel.ID;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.PaymentStatus = orderViewModel.PaymentStatus;
            order.Status = orderViewModel.Status;
            order.Total = orderViewModel.TotalPrice;
        }

        /// <summary>
        /// The CopyFromOrderDetailsDTO
        /// </summary>
        /// <param name="orderDetailViewModel">The orderDetailViewModel<see cref="OrderDetailViewModel"/></param>
        /// <param name="orderDto">The orderDto<see cref="OrderDetailDTO"/></param>
        public static void CopyFromOrderDetailsDTO(this OrderDetailViewModel orderDetailViewModel,
            OrderDetailDTO orderDto)
        {
            orderDetailViewModel.OrderID = orderDto.OrderID;
            orderDetailViewModel.ProductID = orderDto.ProductID;
            orderDetailViewModel.Quantitty = orderDto.Quantitty;
            orderDetailViewModel.ProductName = orderDto.ProductName;
        }

        /// <summary>
        /// The CopyFromOrderDetailsViewModel
        /// </summary>
        /// <param name="orderDetail">The orderDetail<see cref="OrderDetail"/></param>
        /// <param name="orderDetailViewModel">The orderDetailViewModel<see cref="OrderDetailViewModel"/></param>
        public static void CopyFromOrderDetailsViewModel(this OrderDetail orderDetail,
            OrderDetailViewModel orderDetailViewModel)
        {
            orderDetail.OrderID = orderDetailViewModel.OrderID;
            orderDetail.ProductID = orderDetailViewModel.ProductID;
            orderDetail.Quantitty = orderDetailViewModel.Quantitty;
        }
    }
}
