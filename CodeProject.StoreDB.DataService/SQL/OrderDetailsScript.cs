namespace CodeProject.StoreDB.DataService.SQL
{
    public class OrderDetailsScript
    {
        public static string GetAll
        {
            get
            {
                string query = @"SELECT [OrderDetails].*, [Products].Name AS ProductName
                              FROM [OrderDetails]
                              LEFT JOIN [Products] ON [OrderDetails].ProductId = [Products].ID";
                return query;
            }
        }

        public static string GetById
        {
            get
            {
                string query = @"SELECT [OrderDetails].*, [Products].Name AS ProductName
                              FROM [OrderDetails]
                              LEFT JOIN [Products] ON [OrderDetails].ProductId = [Products].ID
                              WHERE ID = @ID ";
                return query;
            }
        }

        public static string GetManyByOrderId
        {
            get
            {
                string query = @"SELECT [OrderDetails].*, [Products].Name AS ProductName
                              FROM [OrderDetails]
                              LEFT JOIN [Products] ON [OrderDetails].ProductId = [Products].ID
                              WHERE OrderID = @OrderID ";
                return query;
            }
        }

        public static string Insert
        {
            get
            {
                string query = @"INSERT INTO [CodeProject.StoreDB.Portal].[dbo].[OrderDetails]
                                       ([OrderID]
                                       ,[ProductID]
                                       ,[Quantitty])
                                 VALUES
                                       (@OrderID
                                       ,@ProductID
                                       ,@Quantitty)";

                return query;
            }
        }

        public static string Delete
        {
            get
            {
                string query = @"DELETE FROM OrderDetails WHERE ID = @ID";
                return query;
            }
        }

        public static string GetCount
        {
            get
            {
                return "SELECT COUNT(*) FROM OrderDetails";
            }
        }
    }
}