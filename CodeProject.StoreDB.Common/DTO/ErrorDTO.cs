namespace CodeProject.StoreDB.Model.DTO
{
    using System;

    public partial class ErrorDTO
    {
        public int ID { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}