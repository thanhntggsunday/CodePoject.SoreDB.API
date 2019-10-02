namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartItem
    {
        public int CartItemId { get; set; }

        [Required]
        public string CartId { get; set; }

        public long ProductId { get; set; }

        public int Count { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }
    }
}
