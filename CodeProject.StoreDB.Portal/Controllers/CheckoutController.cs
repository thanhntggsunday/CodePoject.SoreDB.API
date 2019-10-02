namespace CodeProject.StoreDB.Portal.Controllers
{
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using CodeProject.StoreDB.Portal.Models.BusinessModels;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using CodeProject.StoreDB.Model.DTO;

    /// <summary>
    /// Defines the <see cref="CheckoutController" />
    /// </summary>
    [Authorize]
    public class CheckoutController : Controller
    {
        // IOderBusinessService _oderBusinessService;
        /// <summary>
        /// Defines the PromoCode
        /// </summary>
        private const string PromoCode = "FREE";

        /// <summary>
        /// Defines the _orderBusinessModel
        /// </summary>
        private CheckoutBusinessModel _orderBusinessModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutController"/> class.
        /// </summary>
        /// <param name="oderBusinessService">The oderBusinessService<see cref="IOrderBusinessService"/></param>
        public CheckoutController(IOrderBusinessService oderBusinessService)
        {
            _orderBusinessModel = new CheckoutBusinessModel(oderBusinessService);
        }

        //
        // GET: /Checkout/
        /// <summary>
        /// The AddressAndPayment
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult AddressAndPayment()
        {
            OrderViewModel orderViewModel = new OrderViewModel();

            try
            {
                var cartItems = (List<CartItemDTO>)Session["CartItems"];
                var cartTotal = (decimal)Session["CartTotal"];               
                orderViewModel.CartItems = cartItems;
                orderViewModel.CartTotal = cartTotal;
            }
            catch (Exception)
            {
                orderViewModel.CartItems = new List<CartItemDTO>();
                orderViewModel.CartTotal = 0;

                return View(orderViewModel);
            }     

            

            return View(orderViewModel);
        }

        //
        // POST: /Checkout/AddressAndPayment
        /// <summary>
        /// The AddressAndPayment
        /// </summary>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult AddressAndPayment(OrderViewModel orderViewModel)
        {
            try
            {
                if (string.Equals(orderViewModel.PromoCode, PromoCode,
                StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(orderViewModel);
                }
                else
                {
                    var orderId = _orderBusinessModel.AddressAndPayment(orderViewModel, this.HttpContext);

                    // Reset Session:
                    Session["CartItems"] = null;
                    Session["CartTotal"] = null;

                    return RedirectToAction("Complete", "Checkout", new { result = orderId });
                    // return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                //Invalid - redisplay with errors
                return View(orderViewModel);
            }
        }

        //
        // GET: /Checkout/Complete
        /// <summary>
        /// The Complete
        /// </summary>
        /// <param name="result">The result<see cref="object"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Complete(int result)
        {
            // Validate customer owns this order

            OrderViewModel orderViewModel = new OrderViewModel() { ID = result };

           
            return View("Complete", orderViewModel);
        }
    }
}
