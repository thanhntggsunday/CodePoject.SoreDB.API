namespace CodeProject.StoreDB.Common.Utilities
{
    using CodeProject.StoreDB.Model.Entities;

    /// <summary>
    /// Defines the <see cref="Extensions" />
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The CopyFromProductCategory
        /// </summary>
        /// <param name="productCategory">The productCategory<see cref="ProductCategory"/></param>
        /// <param name="productCategoryData">The productCategoryData<see cref="ProductCategory"/></param>
        public static void CopyFromProductCategory(this ProductCategory productCategory, ProductCategory productCategoryData)
        {
            productCategory.Name = productCategoryData.Name;
            productCategory.Alias = productCategoryData.Alias;
            productCategory.Description = productCategoryData.Description;
            productCategory.Image = productCategoryData.Image;
            productCategory.MetaDescription = productCategoryData.MetaDescription;
            productCategory.MetaKeyword = productCategoryData.MetaKeyword;
            productCategory.Status = productCategoryData.Status;
            productCategory.HomeFlag = productCategoryData.HomeFlag;
            productCategory.UpdatedBy = productCategoryData.CreatedBy;
            productCategory.UpdatedDate = productCategoryData.UpdatedDate;
        }

        /// <summary>
        /// The CopyFromProduc
        /// </summary>
        /// <param name="product">The product<see cref="Product"/></param>
        /// <param name="productData">The productData<see cref="Product"/></param>
        public static void CopyFromProduc(this Product product, Product productData)
        {
            product.Name = productData.Name;
            product.Alias = productData.Alias;
            product.Description = productData.Description;
            product.Image = productData.Image;
            product.MetaDescription = productData.MetaDescription;
            product.MetaKeyword = productData.MetaKeyword;
            product.Status = productData.Status;
            product.HomeFlag = productData.HomeFlag;
            product.CategoryID = productData.CategoryID;
            product.Price = productData.Price;
            product.PromotionPrice = productData.PromotionPrice;
            product.Warranty = productData.Warranty;
            product.Content = productData.Content;
            product.MoreImages = productData.MoreImages;
            product.CreatedBy = productData.CreatedBy;
            product.CreatedDate = productData.CreatedDate;
            product.UpdatedDate = productData.UpdatedDate;
            product.UpdatedBy = productData.UpdatedBy;
        }

        /// <summary>
        /// The CopyFromPostCategory
        /// </summary>
        /// <param name="postCategory">The postCategory<see cref="PostCategory"/></param>
        /// <param name="postCategoryData">The postCategoryData<see cref="PostCategory"/></param>
        public static void CopyFromPostCategory(this PostCategory postCategory, PostCategory postCategoryData)
        {
            postCategory.Name = postCategoryData.Name;
            postCategory.Alias = postCategoryData.Alias;
            postCategory.Description = postCategoryData.Description;
            postCategory.Image = postCategoryData.Image;
            postCategory.MetaDescription = postCategoryData.MetaDescription;
            postCategory.MetaKeyword = postCategoryData.MetaKeyword;
            postCategory.Status = postCategoryData.Status;
            postCategory.HomeFlag = postCategoryData.HomeFlag;
            postCategory.CreatedDate = postCategoryData.CreatedDate;
            postCategory.CreatedBy = postCategoryData.CreatedBy;
            postCategory.UpdatedBy = postCategoryData.UpdatedBy;
            postCategory.UpdatedDate = postCategoryData.UpdatedDate;
        }

        /// <summary>
        /// The CopyFromPost
        /// </summary>
        /// <param name="post">The post<see cref="Post"/></param>
        /// <param name="postData">The postData<see cref="Post"/></param>
        public static void CopyFromPost(this Post post, Post postData)
        {
            post.Name = postData.Name;
            post.Alias = postData.Alias;
            post.Description = postData.Description;
            post.Image = postData.Image;
            post.MetaDescription = postData.MetaDescription;
            post.MetaKeyword = postData.MetaKeyword;
            post.Status = postData.Status;
            post.HomeFlag = postData.HomeFlag;
            post.HotFlag = postData.HotFlag;
            post.CategoryID = postData.CategoryID;
            post.Content = postData.Content;
            post.CreatedBy = postData.CreatedBy;
            post.CreatedDate = postData.CreatedDate;
            post.UpdatedDate = postData.UpdatedDate;
            post.UpdatedBy = postData.UpdatedBy;
        }

        /// <summary>
        /// The CopyFromCartItem
        /// </summary>
        /// <param name="cartItem">The cartItem<see cref="CartItem"/></param>
        /// <param name="cartItemData">The cartItemData<see cref="CartItem"/></param>
        public static void CopyFromCartItem(this CartItem cartItem, CartItem cartItemData)
        {
            cartItem.CartId = cartItemData.CartId;
            cartItem.Count = cartItemData.Count;
            cartItem.DateCreated = cartItemData.DateCreated;
            cartItem.ProductId = cartItemData.ProductId;
        }

        /// <summary>
        /// The CopyFromOrder
        /// </summary>
        /// <param name="order">The order<see cref="Order"/></param>
        /// <param name="orderData">The orderData<see cref="Order"/></param>
        public static void CopyFromOrder(this Order order, Order orderData)
        {
            order.CreatedBy = orderData.CreatedBy;
            order.CreatedDate = orderData.CreatedDate;
            order.CustomerAddress = orderData.CustomerAddress;
            order.CustomerEmail = orderData.CustomerEmail;
            order.CustomerMessage = orderData.CustomerMessage;
            order.CustomerMobile = orderData.CustomerMobile;
            order.CustomerName = orderData.CustomerName;
            order.PaymentMethod = orderData.PaymentMethod;
            order.PaymentStatus = orderData.PaymentStatus;
            order.Status = orderData.Status;
            order.Total = orderData.Total;
        }

        /// <summary>
        /// The CopyFromOrderDetail
        /// </summary>
        /// <param name="orderDetail">The orderDetail<see cref="OrderDetail"/></param>
        /// <param name="orderDetailData">The orderDetailData<see cref="OrderDetail"/></param>
        public static void CopyFromOrderDetail(this OrderDetail orderDetail, OrderDetail orderDetailData)
        {
            orderDetail.OrderID = orderDetailData.OrderID;
            orderDetail.ProductID = orderDetailData.ProductID;
            orderDetail.Quantitty = orderDetailData.Quantitty;
        }
    }
}
