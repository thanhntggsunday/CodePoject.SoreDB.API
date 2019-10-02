namespace CodeProject.StoreDB.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string URL { get; set; }

        public int? DisplayOrder { get; set; }

        public int GroupID { get; set; }

        [StringLength(10)]
        public string Target { get; set; }

        public bool Status { get; set; }

        public int? MenuGroup_ID { get; set; }
    }
}
