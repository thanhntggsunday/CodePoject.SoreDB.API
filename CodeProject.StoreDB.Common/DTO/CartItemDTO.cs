namespace CodeProject.StoreDB.Model.DTO
{
    using System;

    public partial class CartItemDTO
    {
        public int CartItemId { get; set; }

        public string CartId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public decimal Price { get; set; }
    }
}