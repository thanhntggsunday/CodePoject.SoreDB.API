namespace CodeProject.StoreDB.Model.DTO
{
    public partial class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantitty { get; set; }
        public string ProductName { get; set; }
    }
}