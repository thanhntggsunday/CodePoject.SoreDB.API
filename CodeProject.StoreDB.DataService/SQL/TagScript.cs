namespace CodeProject.StoreDB.DataService.SQL
{
    public class TagScript
    {
        public static string GetAll
        {
            get
            {
                string query = @"SELECT* FROM Tags";
                return query;
            }
        }

        public static string GetById
        {
            get
            {
                string query = @"SELECT* FROM Tags WHERE ID = @ID";
                return query;
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[Tags]
                                       ([ID]
                                       ,[Name]
                                       ,[Type])
                                 VALUES
                                       (@ID
                                       ,@Name
                                       ,@Type)

                                SELECT SCOPE_IDENTITY()";

                return query;
            }
        }

        public static string Update
        {
            get
            {
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[Tags]
                                   SET
                                      [Name] = @Name
                                      ,[Type] = @Type
                                 WHERE ID = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                string query = @"DELETE FROM Tags WHERE ID = @ID";
                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM Tags";
            }
        }
    }
}