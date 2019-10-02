namespace CodeProject.StoreDB.Portal.Controllers
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.BusinessSevice.BusinessServiceModels;
    using CodeProject.StoreDB.Portal.Models.BusinessModels;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="ShoppingCartController" />
    /// </summary>
    public class ShoppingCartController : Controller
    {
        /// <summary>
        /// Defines the _shoppingCartBusinessModel
        /// </summary>
        private ShoppingCartBusinessModel _shoppingCartBusinessModel;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartController"/> class.
        /// </summary>
        /// <param name="productBusinessService">The productBusinessService<see cref="IProductBusinessService"/></param>
        public ShoppingCartController(IProductBusinessService productBusinessService, 
            IShoppingCartBusinessService shoppingCartBusinessService)
        {
            _shoppingCartBusinessModel = new ShoppingCartBusinessModel(productBusinessService, 
                                                shoppingCartBusinessService);
        }

        //
        // GET: /ShoppingCart/
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = _shoppingCartBusinessModel.GetCartViewModel(this.HttpContext);

            Session["CartItems"] = viewModel.CartItems;
            Session["CartTotal"] = viewModel.CartTotal;

            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        /// <summary>
        /// The AddToCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult AddToCart(int id)
        {
            _shoppingCartBusinessModel.AddToCart(id, this.HttpContext);
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        /// <summary>
        /// The RemoveFromCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var results = _shoppingCartBusinessModel.RemoveFromCart(id, this.HttpContext);
            return Json(results);
        }

        /// <summary>
        /// The SaveUpdateCart
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <param name="count">The count<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        [HttpPost]
        public string SaveUpdateCart(int id, int count)
        {
            // Remove the item from the cart
            var results = _shoppingCartBusinessModel.SaveUpdateCart(id, count, this.HttpContext);

            return results;
        }

        //
        // GET: /ShoppingCart/CartSummary
        /// <summary>
        /// The CartSummary
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var countCartItems = _shoppingCartBusinessModel.GetCount(this.HttpContext);

            ViewData["CartCount"] = countCartItems;

            return PartialView("CartSummary");
        }
    }
}
