namespace CodeProject.StoreDB.Model.DTO
{
    using System;

    public partial class VisitorStatisticDTO
    {
        public Guid ID { get; set; }
        public DateTime VisitedDate { get; set; }
        public string IPAddress { get; set; }
    }
}