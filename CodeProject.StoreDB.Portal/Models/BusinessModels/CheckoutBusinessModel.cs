namespace CodeProject.StoreDB.Portal.Models.BusinessModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using System;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="CheckoutBusinessModel" />
    /// </summary>
    public class CheckoutBusinessModel
    {
        /// <summary>
        /// Defines the _oderBusinessService
        /// </summary>
        private IOrderBusinessService _oderBusinessService;

        /// <summary>
        /// Defines the PromoCode
        /// </summary>
        private const string PromoCode = "FREE";

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutBusinessModel"/> class.
        /// </summary>
        /// <param name="oderBusinessService">The oderBusinessService<see cref="IOrderBusinessService"/></param>
        public CheckoutBusinessModel(IOrderBusinessService oderBusinessService)
        {
            _oderBusinessService = oderBusinessService;
        }

        /// <summary>
        /// The AddressAndPayment
        /// </summary>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <param name="httpContextBase">The httpContextBase<see cref="HttpContextBase"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int AddressAndPayment(OrderViewModel orderViewModel, HttpContextBase httpContextBase)
        {
            var order = new Order();

            order.CustomerAddress = orderViewModel.CustomerAddress;
            order.CustomerMessage = orderViewModel.CustomerMessage;
            order.CustomerMobile = orderViewModel.CustomerMobile;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.CreatedBy = httpContextBase.User.Identity.Name;
            order.CreatedDate = DateTime.Now;
            order.CustomerName = orderViewModel.CustomerName;
            order.CustomerEmail = orderViewModel.CustomerEmail;
            order.Status = true;
            order.PaymentStatus = "Chưa thanh toán";

            _oderBusinessService.CreateOrder(order, httpContextBase.User.Identity.Name, out TransactionalInformation transactionalInformation);

            return order.ID;
        }

       
    }
}
