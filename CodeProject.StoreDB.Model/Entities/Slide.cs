namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slide
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string Image { get; set; }

        [StringLength(256)]
        public string Url { get; set; }

        public int? DisplayOrder { get; set; }

        public bool Status { get; set; }
    }
}
