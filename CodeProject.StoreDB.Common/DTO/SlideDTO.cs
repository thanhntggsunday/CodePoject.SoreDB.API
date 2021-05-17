namespace CodeProject.StoreDB.Model.DTO
{
    public partial class SlideDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public int? DisplayOrder { get; set; }
        public bool Status { get; set; }
    }
}