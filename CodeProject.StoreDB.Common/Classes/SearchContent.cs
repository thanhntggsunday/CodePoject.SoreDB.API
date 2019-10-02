namespace CodeProject.StoreDB.Common.Classes
{
    /// <summary>
    /// Defines the <see cref="ProductSearchContent" />
    /// </summary>
    public class ProductSearchContent
    {
        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; } = "";

        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the ProductName
        /// </summary>
        public string ProductName { get; set; } = "";

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; } = "";
    }
}
