namespace CodeProject.StoreDB.Model.DTO
{
    using System;

    public partial class OrderDTO
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerMessage { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string PaymentStatus { get; set; }
        public bool? Status { get; set; }
        public decimal? Total { get; set; }
    }
}