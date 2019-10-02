using CodeProject.StoreDB.Interfaces.BLL;
using CodeProject.StoreDB.Model.DTO;
using System.Collections.Generic;

namespace CodeProject.StoreDB.BusinessSevice.BusinessServiceModels
{
    public interface IShoppingCartBusinessService : IBusinessServiceBase
    {
        string ShoppingCartId { get; set; }

        void AddToCart(ProductDTO product);

        void EmptyCart();

        List<CartItemDTO> GetCartItems();

        int GetCount();

        decimal GetTotalPrice();

        void MigrateCart(string userName);

        int RemoveFromCart(int id);

        bool SaveUpdateCart(int id, int count);
    }
}