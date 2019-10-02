namespace CodeProject.StoreDB.DataService.SQL
{
    public class CartItemsScript
    {
        public static string GetAllByShoppingCartId
        {
            get
            {
                string query = @"SELECT [CartItems].*, [Products].Name AS ProductName,
	                                    [Products].Image AS ProductImage, [Products].Price AS Price
                              FROM [CartItems]
                              LEFT JOIN [Products] ON [CartItems].ProductId = [Products].ID
                              WHERE [CartItems].[CartId] = @CartId";
                return query;
            }
        }

        // public static string GetAllPaging { get; }
        public static string GetById
        {
            get
            {
                string query = @"SELECT [CartItems].*, [Products].Name AS ProductName,
	                                    [Products].Image AS ProductImage, [Products].Price AS Price
                              FROM [CartItems]
                              LEFT JOIN [Products] ON [CartItems].ProductId = [Products].ID
                               WHERE [CartItems].[CartItemId] = @CartItemId AND [CartItems].[CartId] = @CartId";
                return query;
            }
        }

        public static string GetByCartIdAndProductId
        {
            get
            {
                string query = @"SELECT [CartItems].*, [Products].Name AS ProductName,
	                                    [Products].Image AS ProductImage, [Products].Price AS Price
                              FROM [CartItems]
                              LEFT JOIN [Products] ON [CartItems].ProductId = [Products].ID
                               WHERE [CartItems].[CartId] = @CartId AND [CartItems].[ProductId] = @ProductId ";
                return query;
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CartItems]
                                       ([CartId]
                                       ,[ProductId]
                                       ,[Count]
                                       ,[DateCreated])
                                 VALUES
                                       (@CartId
                                       ,@ProductId
                                       ,@Count
                                       ,@DateCreated)

                                SELECT  @ID = SCOPE_IDENTITY()";

                return query;
            }
        }

        public static string UpdateCartId
        {
            get
            {
                string query = @"UPDATE [CartItems]
                                   SET [CartId] = @CartId
                                 WHERE [CartItemId] = @ID";
                return query;
            }
        }

        public static string UpdateProductCount
        {
            get
            {
                string query = @"UPDATE [CartItems]
                                   SET
                                      [Count] = @Count
                                 WHERE [CartId] = @CartId  AND [ProductId] = @ProductId";
                return query;
            }
        }

        public static string UpdateProductCountByCartItemId
        {
            get
            {
                string query = @"UPDATE [CartItems]
                                   SET
                                      [Count] = @Count
                                 WHERE [CartItemId] = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                string query = @"DELETE FROM [CartItems] WHERE CartItemId = @ID";
                return query;
            }
        }

        public static string DeleteAllShoppingCart
        {
            get
            {
                string query = @"DELETE FROM [CartItems] WHERE [CartId] = @CartId ";
                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                string query = @"SELECT COUNT(*) FROM [CartItems]";
                return query;
            }
        }
    }
}