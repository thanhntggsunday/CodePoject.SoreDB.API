namespace CodeProject.StoreDB.DataService.SQL
{
    public class ProductScript
    {
        public static string GetAllPaging
        {
            get
            {
                string query = @"
                SELECT Top (@PageSize) p.*
	                                    FROM (SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			                                    LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
	                                    ) p
	                                    Where p.ID Not In
	                                    (select Top (@PageSize * (@PageIndex - 1)) p2.ID FROM
	                                    (SELECT Products.ID AS ID FROM Products LEFT
                                            JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
	                                    ) p2 ORDER BY ID )
	                                    ORDER BY ID

                SELECT  @TotalRows = Count(*)
	            FROM  ( Products LEFT JOIN ProductCategories
                        ON Products.CategoryID = ProductCategories.ID
	                    ) ";

                return query;
            }
        }

        public static string GetAllPagingByCategoryId
        {
            get
            {
                string query = @"SELECT Top (@PageSize) p.*
	                    FROM (SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			                    LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                               Where Products.CategoryID = @CategoryID
	                    ) p
	                    Where p.ID Not In
	                    (select Top (@PageSize * (@PageIndex - 1)) p2.ID FROM
	                    (SELECT Products.ID AS ID FROM Products LEFT
                            JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                           Where Products.CategoryID = @CategoryID
	                    ) p2   ORDER BY ID )
	                    ORDER BY ID

                        SELECT  @TotalRows = Count(*)
	                     FROM  ( Products LEFT JOIN ProductCategories
                                ON Products.CategoryID = ProductCategories.ID
	                             ) Where Products.CategoryID = @CategoryID";

                return query;
            }
        }

        public static string GetAllPagingByProductName
        {
            get
            {
                string query = @"SELECT Top (@PageSize) p.*
	                            FROM (SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			                            LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
	                            ) p
	                            Where (p.Name LIKE @ProductName AND p.ID Not In
	                            (select Top (@PageSize * (@PageIndex - 1)) p2.ID FROM
	                            (SELECT Products.ID AS ID FROM Products LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
		                            Where Products.Name LIKE @ProductName ) p2 ORDER BY ID ))
	                            ORDER BY ID

                        SELECT  @TotalRows = Count(*)
	                    FROM (Products
                              LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                              ) Where Products.Name LIKE @ProductName ";

                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM Products";
            }
        }

        public static string GetById
        {
            get
            {
                return @"SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			             LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                        WHERE Products.ID = @ID";
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[Products]
                                    ([Name]
                                    ,[Alias]
                                    ,[CategoryID]
                                    ,[BrandID]
                                    ,[Image]
                                    ,[MoreImages]
                                    ,[Price]
                                    ,[PromotionPrice]
                                    ,[Warranty]
                                    ,[Description]
                                    ,[Content]
                                    ,[HomeFlag]
                                    ,[HotFlag]
                                    ,[ViewCount]
                                    ,[CreatedDate]
                                    ,[CreatedBy]
                                    ,[UpdatedDate]
                                    ,[UpdatedBy]
                                    ,[MetaKeyword]
                                    ,[MetaDescription]
                                    ,[Status]
                                    ,[Quantity])
                                VALUES
                                    (@Name
                                    ,@Alias
                                    ,@CategoryID
                                    ,@BrandID
                                    ,@Image
                                    ,@MoreImages
                                    ,@Price
                                    ,@PromotionPrice
                                    ,@Warranty
                                    ,@Description
                                    ,@Content
                                    ,@HomeFlag
                                    ,@HotFlag
                                    ,@ViewCount
                                    ,@CreatedDate
                                    ,@CreatedBy
                                    ,@UpdatedDate
                                    ,@UpdatedBy
                                    ,@MetaKeyword
                                    ,@MetaDescription
                                    ,@Status
                                    ,@Quantity)

                            SELECT  @ID = SCOPE_IDENTITY()";

                return query;
            }
        }

        public static string Update
        {
            get
            {
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[Products]
                                        SET [Name] = @Name
                                            ,[Alias] = @Alias
                                            ,[CategoryID] = @CategoryID
                                            ,[BrandID] = @BrandID
                                            ,[Image] = @Image
                                            ,[MoreImages] = @MoreImages
                                            ,[Price] = @Price
                                            ,[PromotionPrice] = @PromotionPrice
                                            ,[Warranty] = @Warranty
                                            ,[Description] = @Description
                                            ,[Content] = @Content
                                            ,[HomeFlag] = @HomeFlag
                                            ,[HotFlag] = @HotFlag
                                            ,[ViewCount] = @ViewCount
                                            ,[CreatedDate] = @CreatedDate
                                            ,[CreatedBy] = @CreatedBy
                                            ,[UpdatedDate] = @UpdatedDate
                                            ,[UpdatedBy] = @UpdatedBy
                                            ,[MetaKeyword] = @MetaKeyword
                                            ,[MetaDescription] = @MetaDescription
                                            ,[Status] = @Status
                                            ,[Quantity] = @Quantity
                                        WHERE [ID] = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                return "DELETE FROM Products WHERE ID = @ID ";
            }
        }

        public static string GetNewProductsByCategoryId
        {
            get
            {
                string query = @"SELECT Top 8  p.*
	                    FROM (SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			                    LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                               Where Products.CategoryID = @CategoryID
	                    ) p
	                    ORDER BY [CreatedDate] DESC ";

                return query;
            }
        }

        public static string GetProductsRelate
        {
            get
            {
                string query = @"SELECT Top 4  p.*
	                    FROM (SELECT Products.*, ProductCategories.Name AS CategoryName FROM Products
			                    LEFT JOIN ProductCategories ON Products.CategoryID = ProductCategories.ID
                               Where Products.CategoryID = @CategoryID AND Products.ID <> @ID
	                    ) p
	                    ORDER BY [CreatedDate] DESC ";

                return query;
            }
        }
    }
}