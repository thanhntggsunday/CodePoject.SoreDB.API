namespace CodeProject.StoreDB.DataService.SQL
{
    public class OrderScript
    {
        public static string GetAll
        {
            get
            {
                string query = @"SELECT * FROM Orders";
                return query;
            }
        }

        public static string GetAllPaging
        {
            get
            {
                string query = @"SELECT Top (@PageSize) *
	                            FROM [Orders]
	                            Where ID Not In
	                            (select Top (@PageSize * (@PageIndex - 1)) ID FROM
	                                [Orders] ORDER BY ID )
	                            ORDER BY ID

                                SELECT  @TotalRows = Count(*) FROM [Orders]";

                return query;
            }
        }

        public static string GetById
        {
            get
            {
                string query = @"SELECT * FROM Orders WHERE ID = @ID";
                return query;
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[Orders]
                                       ([CustomerName]
                                       ,[CustomerAddress]
                                       ,[CustomerEmail]
                                       ,[CustomerMobile]
                                       ,[CustomerMessage]
                                       ,[PaymentMethod]
                                       ,[CreatedDate]
                                       ,[CreatedBy]
                                       ,[PaymentStatus]
                                       ,[Status]
                                       ,[Total])
                                 VALUES
                                       (@CustomerName
                                       ,@CustomerAddress
                                       ,@CustomerEmail
                                       ,@CustomerMobile
                                       ,@CustomerMessage
                                       ,@PaymentMethod
                                       ,@CreatedDate
                                       ,@CreatedBy
                                       ,@PaymentStatus
                                       ,@Status
                                       ,@Total)

                                SELECT  @ID = SCOPE_IDENTITY()";

                return query;
            }
        }

        public static string Update
        {
            get
            {
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[Orders]
                                   SET
                                      [PaymentStatus] = @PaymentStatus
                                      ,[Status] = @Status

                                 WHERE ID = @ID";
                return query;
            }
        }

        public static string UpdateTotal
        {
            get
            {
                string query = @"UPDATE [CodeProject.StoreDB.Portal].[dbo].[Orders]
                                   SET
                                      [Total] = @Total

                                 WHERE ID = @ID";
                return query;
            }
        }

        public static string Delete
        {
            get
            {
                string query = @"DELETE FROM Orders WHERE ID = @ID";
                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM Orders";
            }
        }
    }
}