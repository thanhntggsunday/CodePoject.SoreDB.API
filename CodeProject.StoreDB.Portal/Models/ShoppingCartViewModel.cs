namespace CodeProject.StoreDB.Portal.Models
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ShoppingCartViewModel" />
    /// </summary>
    public class ShoppingCartViewModel : TransactionalInformation
    {
        /// <summary>
        /// Gets or sets the CartItems
        /// </summary>
        public List<CartItemDTO> CartItems { get; set; }

        /// <summary>
        /// Gets or sets the CartTotal
        /// </summary>
        public decimal CartTotal { get; set; }
    }
}
