namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [StringLength(256)]
        public string MetaKeyword { get; set; }

        [StringLength(256)]
        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}
