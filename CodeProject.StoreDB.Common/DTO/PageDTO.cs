namespace CodeProject.StoreDB.Model.DTO
{
    using System;

    public partial class PageDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool Status { get; set; }
    }
}