namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Color
    {
        public int ColorID { get; set; }

        [StringLength(50)]
        public string ColorName { get; set; }

        [StringLength(100)]
        public string Photo { get; set; }
    }
}
