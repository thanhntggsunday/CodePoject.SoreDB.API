namespace CodeProject.StoreDB.Portal.Models.BusinessModels
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.BusinessSevice.BusinessService;
    using CodeProject.StoreDB.BusinessSevice.BusinessServiceModels;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.DataService.UnitOfWork;
    using CodeProject.StoreDB.Model.DTO;
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="ShoppingCartBusinessModel" />
    /// </summary>
    public class ShoppingCartBusinessModel
    {
        /// <summary>
        /// Gets or sets the ShoppingCartId
        /// </summary>
        private string ShoppingCartId { get; set; }

        /// <summary>
        /// Defines the CartSessionKey
        /// </summary>
        public const string CartSessionKey = "CartId";

        /// <summary>
        /// Defines the _productBusinessService
        /// </summary>
        private IProductBusinessService _productBusinessService;

        private IShoppingCartBusinessService _shoppingCartBusinessService;

        public ShoppingCartBusinessModel()
        {
            _productBusinessService = null;

            _productBusinessService = new ProductBusinessService(new UnitOfWork());

            _shoppingCartBusinessService = null;

            _shoppingCartBusinessService = new ShoppingCartBusinessService(new UnitOfWork());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartBusinessModel"/> class.
        /// </summary>
        /// <param name="productBusinessService">The productBusinessService<see cref="IProductBusinessService"/></param>
        public ShoppingCartBusinessModel(IProductBusinessService productBusinessService,
            IShoppingCartBusinessService shoppingCartBusinessService)
        {
            _productBusinessService = productBusinessService;
            _shoppingCartBusinessService = shoppingCartBusinessService;
        }

        /// <summary>
        /// The GetCart
        /// </summary>
        /// <param name="context">The context<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="ShoppingCartBusinessService"/></returns>
        private IShoppingCartBusinessService GetCart(HttpContextBase context)
        {
            var cartId = GetCartId(context);

            _shoppingCartBusinessService.ShoppingCartId = cartId;

            return _shoppingCartBusinessService;
        }

        private decimal GetTotalPrice(List<CartItemDTO> cartItems)
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            decimal total = 0;

            foreach (var item in cartItems)
            {
                total += item.Count * item.Price;
            }

            return total;
        }

        /// <summary>
        /// The GetCartViewModel
        /// </summary>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="ShoppingCartViewModel"/></returns>
        public ShoppingCartViewModel GetCartViewModel(HttpContextBase httpContextBase)
        {
            IShoppingCartBusinessService cart = GetCart(httpContextBase);
            // Set up our ViewModel
            var CartItems = cart.GetCartItems();
            var CartTotal = GetTotalPrice(CartItems);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = CartItems,
                CartTotal = CartTotal
            };

            return viewModel;
        }

        /// <summary>
        /// The AddToCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        public void AddToCart(int id, HttpContextBase httpContextBase)
        {
            IShoppingCartBusinessService cart = GetCart(httpContextBase);

            var prdoduct = _productBusinessService.GetProduct(id, out TransactionalInformation transactionalInformation);

            cart.AddToCart(prdoduct);
        }

        /// <summary>
        /// The RemoveFromCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="ShoppingCartRemoveViewModel"/></returns>
        public ShoppingCartRemoveViewModel RemoveFromCart(int id, HttpContextBase httpContextBase)
        {
            // Remove the item from the cart
            IShoppingCartBusinessService cart = GetCart(httpContextBase);

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = " has been removed from your shopping cart.",
                // CartTotal = cart.GetTotalPrice(),
                // CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return results;
        }

        /// <summary>
        /// The GetCount
        /// </summary>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="object"/></returns>
        internal object GetCount(HttpContextBase httpContextBase)
        {
            IShoppingCartBusinessService cart = GetCart(httpContextBase);

            return cart.GetCount();
        }

        /// <summary>
        /// The SaveUpdateCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="count">The count<see cref="int"/></param>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string SaveUpdateCart(int id, int count, HttpContextBase httpContextBase)
        {
            // Remove the item from the cart
            IShoppingCartBusinessService cart = GetCart(httpContextBase);

            bool b = cart.SaveUpdateCart(id, count);

            if (b)
            {
                return "success!";
            }
            return "falied";
        }

        // We're using HttpContextBase to allow access to cookies.
        /// <summary>
        /// The GetCartId
        /// </summary>
        /// <param name="context">The context<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class

                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        /// <summary>
        /// The MigrateCart
        /// </summary>
        /// <param name="userName">The userName<see cref="string"/></param>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        public void MigrateCart(string userName, HttpContextBase httpContextBase)
        {
            IShoppingCartBusinessService cart = GetCart(httpContextBase);

            cart.MigrateCart(userName);
        }

        /// <summary>
        /// The ResetSession
        /// </summary>
        /// <param name="context">The context<see cref="HttpContextBase"/></param>
        public  void ResetSession(HttpContextBase context)
        {
            context.Session[CartSessionKey] = null;
            _shoppingCartBusinessService.CloseSession();
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}