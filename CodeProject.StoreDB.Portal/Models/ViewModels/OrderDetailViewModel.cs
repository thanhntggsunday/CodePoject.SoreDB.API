namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="OrderDetailViewModel" />
    /// </summary>
    public class OrderDetailViewModel : TransactionalInformation
    {
        /// <summary>
        /// Gets or sets the OrderID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the ProductID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the Quantitty
        /// </summary>
        public int Quantitty { get; set; }

        /// <summary>
        /// Gets or sets the ProductName
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the OrderDetailDTOs
        /// </summary>
        public List<OrderDetailDTO> OrderDetailDTOs { get; set; }
    }
}
