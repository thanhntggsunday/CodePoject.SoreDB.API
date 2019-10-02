namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brand
    {
        public int BrandID { get; set; }

        [Required]
        [StringLength(100)]
        public string BrandName { get; set; }

        [StringLength(100)]
        public string Logo { get; set; }
    }
}
