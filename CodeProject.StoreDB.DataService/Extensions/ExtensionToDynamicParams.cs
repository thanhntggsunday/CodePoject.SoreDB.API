using CodeProject.StoreDB.Model.Entities;
using Dapper;
using System.Data;

namespace CodeProject.StoreDB.DataService.Extensions
{
    public static class ExtensionToDynamicParams
    {
        #region Product:

        private static DynamicParameters ToDynamicParametersForObjectNoId(Product product)
        {
            var param = new DynamicParameters();
            param.Add("@Name", product.Name);
            param.Add("@Alias", product.Alias);
            param.Add("@CategoryID", product.CategoryID);
            param.Add("@BrandID", product.BrandID);
            param.Add("@Content", product.Content);
            param.Add("@CreatedBy", product.CreatedBy);
            param.Add("@CreatedDate", product.CreatedDate);
            param.Add("@Description", product.Description);
            param.Add("@HomeFlag", product.HomeFlag);
            param.Add("@HotFlag", product.HotFlag);
            param.Add("@Image", product.Image);
            param.Add("@MetaDescription", product.MetaDescription);
            param.Add("@MetaKeyword", product.MetaKeyword);
            param.Add("@MoreImages", product.MoreImages);
            param.Add("@Price", product.Price);
            param.Add("@PromotionPrice", product.PromotionPrice);
            param.Add("@Quantity", product.Quantity);
            param.Add("@Status", product.Status);
            param.Add("@UpdatedBy", product.UpdatedBy);
            param.Add("@UpdatedDate", product.UpdatedDate);
            param.Add("@ViewCount", product.ViewCount);
            param.Add("@Warranty", product.Warranty);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this Product product)
        {
            var param = ToDynamicParametersForObjectNoId(product);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this Product product)
        {
            var param = ToDynamicParametersForObjectNoId(product);

            param.Add("@ID", product.ID);

            return param;
        }

        #endregion Product:

        #region Post:

        private static DynamicParameters ToDynamicParametersForObjectNoId(Post post)
        {
            var param = new DynamicParameters();
            param.Add("@Name", post.Name);
            param.Add("@Alias", post.Alias);
            param.Add("@CategoryID", post.CategoryID);
            param.Add("@Content", post.Content);
            param.Add("@CreatedBy", post.CreatedBy);
            param.Add("@CreatedDate", post.CreatedDate);
            param.Add("@Description", post.Description);
            param.Add("@HomeFlag", post.HomeFlag);
            param.Add("@HotFlag", post.HotFlag);
            param.Add("@Image", post.Image);
            param.Add("@MetaDescription", post.MetaDescription);
            param.Add("@MetaKeyword", post.MetaKeyword);
            param.Add("@Status", post.Status);
            param.Add("@UpdatedBy", post.UpdatedBy);
            param.Add("@UpdatedDate", post.UpdatedDate);
            param.Add("@ViewCount", post.ViewCount);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this Post post)
        {
            var param = ToDynamicParametersForObjectNoId(post);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this Post post)
        {
            var param = ToDynamicParametersForObjectNoId(post);

            param.Add("@ID", post.ID);

            return param;
        }

        #endregion Post:

        #region ProductCategory

        private static DynamicParameters ToDynamicParametersForObjectNoId(ProductCategory category)
        {
            var param = new DynamicParameters();
            param.Add("@Name", category.Name);
            param.Add("@Alias", category.Alias);
            param.Add("@CreatedBy", category.CreatedBy);
            param.Add("@CreatedDate", category.CreatedDate);
            param.Add("@Description", category.Description);
            param.Add("@HomeFlag", category.HomeFlag);
            param.Add("@Image", category.Image);
            param.Add("@MetaDescription", category.MetaDescription);
            param.Add("@MetaKeyword", category.MetaKeyword);
            param.Add("@Status", category.Status);
            param.Add("@UpdatedBy", category.UpdatedBy);
            param.Add("@UpdatedDate", category.UpdatedDate);
            param.Add("@DisplayOrder", category.DisplayOrder);
            param.Add("@ParentID", category.ParentID);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this ProductCategory category)
        {
            var param = ToDynamicParametersForObjectNoId(category);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this ProductCategory category)
        {
            var param = ToDynamicParametersForObjectNoId(category);

            param.Add("@ID", category.ID);

            return param;
        }

        #endregion ProductCategory

        #region PostCategory

        private static DynamicParameters ToDynamicParametersForObjectNoId(PostCategory category)
        {
            var param = new DynamicParameters();
            param.Add("@Name", category.Name);
            param.Add("@Alias", category.Alias);
            param.Add("@CreatedBy", category.CreatedBy);
            param.Add("@CreatedDate", category.CreatedDate);
            param.Add("@Description", category.Description);
            param.Add("@HomeFlag", category.HomeFlag);
            param.Add("@Image", category.Image);
            param.Add("@MetaDescription", category.MetaDescription);
            param.Add("@MetaKeyword", category.MetaKeyword);
            param.Add("@Status", category.Status);
            param.Add("@UpdatedBy", category.UpdatedBy);
            param.Add("@UpdatedDate", category.UpdatedDate);
            param.Add("@ParentID", category.ParentID);
            param.Add("@DisplayOrder", category.DisplayOrder);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this PostCategory category)
        {
            var param = ToDynamicParametersForObjectNoId(category);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this PostCategory category)
        {
            var param = ToDynamicParametersForObjectNoId(category);

            param.Add("@ID", category.ID);

            return param;
        }

        #endregion PostCategory

        #region Order:

        private static DynamicParameters ToDynamicParametersForObjectNoId(Order order)
        {
            var param = new DynamicParameters();
            param.Add("@CustomerAddress", order.CustomerAddress);
            param.Add("@CustomerEmail", order.CustomerEmail);
            param.Add("@CreatedBy", order.CreatedBy);
            param.Add("@CreatedDate", order.CreatedDate);
            param.Add("@CustomerMessage", order.CustomerMessage);
            param.Add("@CustomerMobile", order.CustomerMobile);
            param.Add("@CustomerName", order.CustomerName);
            param.Add("@PaymentMethod", order.PaymentMethod);
            param.Add("@PaymentStatus", order.PaymentStatus);
            param.Add("@Status", order.Status);
            param.Add("@Total", order.Total);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this Order order)
        {
            var param = ToDynamicParametersForObjectNoId(order);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this Order order)
        {
            var param = new DynamicParameters();

            param.Add("@PaymentStatus", order.PaymentStatus);
            param.Add("@Status", order.Status);

            param.Add("@ID", order.ID);

            return param;
        }

        #endregion Order:

        #region Order Details:

        private static DynamicParameters ToDynamicParametersForObjectNoId(this OrderDetail orderDetail)
        {
            var param = new DynamicParameters();
            param.Add("@OrderID", orderDetail.OrderID);
            param.Add("@ProductID", orderDetail.ProductID);
            param.Add("@Quantitty", orderDetail.Quantitty);

            return param;
        }

        #endregion Order Details:

        #region CartItems:

        private static DynamicParameters ToDynamicParametersForObjectNoId(CartItem cartItem)
        {
            var param = new DynamicParameters();
            param.Add("@CartId", cartItem.CartId);
            param.Add("@Count", cartItem.Count);
            param.Add("@DateCreated", cartItem.DateCreated);
            param.Add("@ProductId", cartItem.ProductId);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForInsert(this CartItem cartItem)
        {
            var param = ToDynamicParametersForObjectNoId(cartItem);

            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return param;
        }

        public static DynamicParameters ToDynamicParametersForUpdate(this CartItem cartItem)
        {
            var param = new DynamicParameters();

            param.Add("@ID", cartItem.CartItemId);

            return param;
        }

        #endregion CartItems:
    }
}