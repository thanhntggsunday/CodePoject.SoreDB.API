namespace CodeProject.StoreDB.BusinessSevice.BusinessServiceModels
{
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.DataService.SQL;
    using CodeProject.StoreDB.DataService.UnitOfWork;
    using CodeProject.StoreDB.Model.DTO;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="ShoppingCartBusinessService" />
    /// </summary>
    public class ShoppingCartBusinessService : IShoppingCartBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Gets or sets the ShoppingCartId
        /// </summary>
        public string ShoppingCartId { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="ShoppingCartBusinessService"/> class from being created.
        /// </summary>
        private ShoppingCartBusinessService()
        {
            _dataService = new UnitOfWork();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public ShoppingCartBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The GetCart
        /// </summary>
        /// <param name="ShoppingCartId">The ShoppingCartId<see cref="string"/></param>
        /// <returns>The <see cref="ShoppingCartBusinessService"/></returns>
        public static ShoppingCartBusinessService GetCart(string ShoppingCartId)
        {
            var cart = new ShoppingCartBusinessService();
            cart.ShoppingCartId = ShoppingCartId;
            return cart;
        }

        /// <summary>
        /// The AddToCart
        /// </summary>
        /// <param name="product">The product<see cref="ProductDTO"/></param>
        public void AddToCart(ProductDTO product)
        {
            // Get the matching cart and album instances
            _dataService.CreateSession();

            var param = new DynamicParameters();
            param.Add("@CartId", ShoppingCartId);
            param.Add("@ProductId", product.ID);

            var cartItem = _dataService.CartItemRepository.GetById(CartItemsScript.GetByCartIdAndProductId,
                                param, CommandType.Text);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                var insertParam = new DynamicParameters();
                insertParam.Add("@CartId", ShoppingCartId);
                insertParam.Add("@ProductId", product.ID);
                insertParam.Add("@Count", 1);
                insertParam.Add("@DateCreated", DateTime.Now);
                insertParam.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                _dataService.CartItemRepository.Insert(CartItemsScript.Insert, insertParam, CommandType.Text);

                var iD = insertParam.Get<int>("@ID");
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                var updateParam = new DynamicParameters();
                var count = cartItem.Count + 1;
                updateParam.Add("@CartId", ShoppingCartId);
                updateParam.Add("@ProductId", product.ID);
                updateParam.Add("@Count", count);

                _dataService.CartItemRepository.Update(CartItemsScript.UpdateProductCount, updateParam, CommandType.Text);
            }
            // Save changes
            _dataService.CommitTransaction(true);
            _dataService.CloseSession();
        }

        /// <summary>
        /// The RemoveFromCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int RemoveFromCart(int id)
        {
            // Get the cart
            _dataService.CreateSession();

            var param = new DynamicParameters();
            param.Add("@CartId", ShoppingCartId);
            param.Add("@CartItemId", id);

            var cartItem = _dataService.CartItemRepository.GetById(CartItemsScript.GetById,
                                param, CommandType.Text);

            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                    // If the item does exist in the cart, then add one to the quantity
                    var updateParam = new DynamicParameters();
                    var count = cartItem.Count--;
                    updateParam.Add("@CartId", ShoppingCartId);
                    updateParam.Add("@ProductId", cartItem.ProductId);
                    updateParam.Add("@Count", count);

                    _dataService.CartItemRepository.Update(CartItemsScript.UpdateProductCount, 
                        updateParam, CommandType.Text);
                }
                else
                {
                    var deleteParam = new DynamicParameters();
                    deleteParam.Add("@ID", id);

                    _dataService.CartItemRepository.Delete(CartItemsScript.Delete, 
                        deleteParam, CommandType.Text);
                }
                // Save changes
                _dataService.CommitTransaction(true);
                _dataService.CloseSession();
            }
            return itemCount;
        }

        /// <summary>
        /// The SaveUpdateCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="count">The count<see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool SaveUpdateCart(int id, int count)
        {
            _dataService.CreateSession();

            var param = new DynamicParameters();
            param.Add("@Count", count);
            param.Add("@ID", id);

            _dataService.CartItemRepository.Update(CartItemsScript.UpdateProductCountByCartItemId,
                                param, CommandType.Text);

            _dataService.CommitTransaction(true);
            _dataService.CloseSession();

            return true;
        }

        /// <summary>
        /// The EmptyCart
        /// </summary>
        public void EmptyCart()
        {
            _dataService.CreateSession();

            var param = new DynamicParameters();
            param.Add("@CartId", ShoppingCartId);

            _dataService.CartItemRepository.Delete(CartItemsScript.DeleteAllShoppingCart,
                                                    param, CommandType.Text);

            _dataService.CommitTransaction(true);
            _dataService.CloseSession();
        }

        /// <summary>
        /// The GetCartItems
        /// </summary>
        /// <returns>The <see cref="List{CartItemDTO}"/></returns>
        public List<CartItemDTO> GetCartItems()
        {
            _dataService.CreateSession();

            var cartItems = new List<CartItemDTO>();

            var param = new DynamicParameters();
            param.Add("@CartId", ShoppingCartId);

            var results = _dataService.CartItemRepository.GetManyRows(CartItemsScript.GetAllByShoppingCartId,
                                                            param, CommandType.Text);

            if (results != null)
            {
                foreach (var item in results)
                {
                    cartItems.Add(new CartItemDTO
                    {
                        CartId = item.CartId,
                        CartItemId = item.CartItemId,
                        Count = item.Count,
                        DateCreated = item.DateCreated,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        ProductImage = item.ProductImage,
                        ProductName = item.ProductName
                    });
                }
            }

            _dataService.CloseSession();

            return cartItems;
        }

        /// <summary>
        /// The GetCount
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            var cartItems = GetCartItems();
            int count = 0;

            foreach (var item in cartItems)
            {
                count += item.Count;
            }
            // Return 0 if all entries are null
            return count;
        }

        /// <summary>
        /// The GetTotalPrice
        /// </summary>
        /// <returns>The <see cref="decimal"/></returns>
        public decimal GetTotalPrice()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            var cartItems = GetCartItems();
            decimal total = 0;

            foreach (var item in cartItems)
            {
                total += item.Count * item.Price;
            }

            return total;
        }

        /// <summary>
        /// The MigrateCart
        /// </summary>
        /// <param name="userName">The userName<see cref="string"/></param>
        public void MigrateCart(string userName)
        {
            var shoppingCart = GetCartItems();

            _dataService.CreateSession();

            foreach (var item in shoppingCart)
            {
                var param = new DynamicParameters();
                param.Add("@CartId", userName);
                param.Add("@ID", item.CartItemId);

                _dataService.CartItemRepository.Update(CartItemsScript.UpdateCartId, param, CommandType.Text);
            }

            _dataService.CommitTransaction(true);
            _dataService.CloseSession();
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
        }

        public void CloseSession()
        {
            _dataService.CloseSession();
        }
    }
}