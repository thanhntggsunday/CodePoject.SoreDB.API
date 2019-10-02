namespace CodeProject.StoreDB.DataService.SQL
{
    public class PostScript
    {
        public static string GetAllPaging
        {
            get
            {
                string query = @"SELECT Top (@PageSize) p.*
	                    FROM (SELECT Posts.*, PostCategories.Name AS CategoryName FROM Posts
			                    LEFT JOIN PostCategories ON Posts.CategoryID = PostCategories.ID
	                    ) p
	                    Where p.ID Not In
	                    (select Top (@PageSize * (@PageIndex - 1)) p2.ID FROM
	                    (SELECT Posts.ID AS ID FROM Posts LEFT
                            JOIN PostCategories ON Posts.CategoryID = PostCategories.ID
	                    ) p2 ORDER BY ID )
	                    ORDER BY ID 

                        SELECT  @TotalRows = Count(*)
	                    FROM  ( Posts LEFT JOIN PostCategories
                                ON Posts.CategoryID = PostCategories.ID
	                            ) ";

                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM Posts";
            }
        }

        public static string GetById
        {
            get
            {
                return "SELECT * FROM Posts WHERE ID = @ID";
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[Posts]
                                   ([Name]
                                   ,[Alias]
                                   ,[CategoryID]
                                   ,[Image]
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
                                   ,[Status])
                             VALUES
                                   (@Name
                                   ,@Alias
                                   ,@CategoryID
                                   ,@Image
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
                                   ,@Status)

                        SELECT  @ID = SCOPE_IDENTITY()";

                return query;
            }
        }

        public static string Update
        {
            get
            {
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[Posts]
                                        SET [Name] = @Name
                                            ,[Alias] = @Alias
                                            ,[CategoryID] = @CategoryID
                                            ,[Image] = @Image
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

                                        WHERE [ID] = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                return "DELETE FROM Posts WHERE ID = @ID ";
            }
        }
    }
}