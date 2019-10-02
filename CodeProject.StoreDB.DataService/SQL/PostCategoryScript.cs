namespace CodeProject.StoreDB.DataService.SQL
{
    public class PostCategoryScript
    {
        public static string GetAll
        {
            get
            {
                string query = @"SELECT * FROM PostCategories";
                return query;
            }
        }

        public static string GetAllPaging { get; }

        public static string GetById
        {
            get
            {
                string query = @"SELECT * FROM PostCategories WHERE ID = @ID";
                return query;
            }
        }

        public static string GetMany { get; }
        public static string DeleteMany { get; }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[PostCategories]
                                               ([Name]
                                               ,[Alias]
                                               ,[Description]
                                               ,[ParentID]
                                               ,[DisplayOrder]
                                               ,[Image]
                                               ,[HomeFlag]
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
                                               ,@Description
                                               ,@ParentID
                                               ,@DisplayOrder
                                               ,@Image
                                               ,@HomeFlag
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
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[PostCategories]
                                   SET [Name] = @Name
                                      ,[Alias] = @Alias
                                      ,[Description] = @Description
                                      ,[ParentID] = @ParentID
                                      ,[DisplayOrder] = @DisplayOrder
                                      ,[Image] = @Image
                                      ,[HomeFlag] = @HomeFlag
                                      ,[CreatedDate] = @CreatedDate
                                      ,[CreatedBy] = @CreatedBy
                                      ,[UpdatedDate] = @UpdatedDate
                                      ,[UpdatedBy] = @UpdatedBy
                                      ,[MetaKeyword] = @MetaKeyword
                                      ,[MetaDescription] = @MetaDescription
                                      ,[Status] = @Status
                                 WHERE ID = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                string query = @"DELETE FROM PostCategories WHERE ID = @ID";
                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM PostCategories";
            }
        }
    }
}