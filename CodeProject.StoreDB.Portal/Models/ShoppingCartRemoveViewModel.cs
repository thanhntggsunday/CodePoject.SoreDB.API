namespace CodeProject.StoreDB.Portal.Models
{
    using CodeProject.StoreDB.Common.Classes;

    /// <summary>
    /// Defines the <see cref="ShoppingCartRemoveViewModel" />
    /// </summary>
    public class ShoppingCartRemoveViewModel : TransactionalInformation
    {
        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the CartTotal
        /// </summary>
        public decimal CartTotal { get; set; }

        /// <summary>
        /// Gets or sets the CartCount
        /// </summary>
        public int CartCount { get; set; }

        /// <summary>
        /// Gets or sets the ItemCount
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// Gets or sets the DeleteId
        /// </summary>
        public int DeleteId { get; set; }
    }
}
