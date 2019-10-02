namespace CodeProject.StoreDB.Portal.API
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.Utilities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="OrderServiceController" />
    /// </summary>
    [RoutePrefix("api/OrderService")]
    public class OrderServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _orderBusinessService
        /// </summary>
        private IOrderBusinessService _orderBusinessService;

        /// <summary>
        /// Defines the _orderDetailBusinessService
        /// </summary>
        private IOrderDetailBusinessService _orderDetailBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderServiceController"/> class.
        /// </summary>
        /// <param name="orderBusinessService">The orderBusinessService<see cref="IOrderBusinessService"/></param>
        /// <param name="orderDetailBusinessService">The orderDetailBusinessService<see cref="IOrderDetailBusinessService"/></param>
        public OrderServiceController(IOrderBusinessService orderBusinessService,
            IOrderDetailBusinessService orderDetailBusinessService)
        {
            _orderBusinessService = orderBusinessService;
            _orderDetailBusinessService = orderDetailBusinessService;
        }

        /// <summary>
        /// The GetOrders
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAllPaging")]
        [HttpPost]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request,
           [FromBody] OrderViewModel orderViewModel)
        {
            var transaction = new TransactionalInformation();

            int currentPageNumber = orderViewModel.Pager.CurrentPageNumber;

            int pageSize = orderViewModel.Pager.PageSize;

            var orderDTOs = _orderBusinessService
                                    .GetOrdersByPaging(currentPageNumber, pageSize, "ID", "ASC", out transaction);

            if (transaction.ReturnStatus == false)
            {
                orderViewModel.ReturnStatus = false;
                orderViewModel.ReturnMessage = transaction.ReturnMessage;
                orderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, orderViewModel);
                return responseError;
            }
            else
            {
                orderViewModel.ReturnStatus = true;
                orderViewModel.ReturnMessage = transaction.ReturnMessage;
                orderViewModel.orderDTOs = orderDTOs;
                orderViewModel.Pager = transaction.Pager;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
            return response;
        }

        /// <summary>
        /// The UpdateOrder
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request,
           [FromBody] OrderViewModel orderViewModel)
        {
            var transaction = new TransactionalInformation();
            Order order = new Order();
            HttpResponseMessage response;

            order.CopyFromOrderViewModel(orderViewModel);

            _orderBusinessService.UpdateOrder(order, out transaction);

            if (transaction.ReturnStatus == false)
            {
                orderViewModel.ReturnStatus = false;
                orderViewModel.ReturnMessage = transaction.ReturnMessage;
                orderViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, orderViewModel);
                return responseError;
            }
            else
            {
                orderViewModel.ReturnStatus = true;
                orderViewModel.ReturnMessage = transaction.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
            }

            return response;
        }

        /// <summary>
        /// The GetOrderById
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="orderViewModel">The orderViewModel<see cref="OrderViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetById")]
        [HttpPost]
        public HttpResponseMessage GetById(HttpRequestMessage request,
              [FromBody] OrderViewModel orderViewModel)
        {
            var transactionalInformation = new TransactionalInformation();
            HttpResponseMessage response;
            List<OrderDetailDTO> orderDetailDTOs;

            var orderId = orderViewModel.ID;
            var orderDto = _orderBusinessService.GetOrder(orderId, out transactionalInformation, out orderDetailDTOs);

           
            if (transactionalInformation.ReturnStatus == false)
            {
                orderViewModel.ReturnStatus = false;
                orderViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                orderViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, orderViewModel);
                return responseError;
            }
            else
            {
                orderViewModel.CopyFromOrderDTO(orderDto);
                orderViewModel.OrderDetailDTOs = orderDetailDTOs;
                orderViewModel.ReturnStatus = true;
                orderViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, orderViewModel);
            }

            return response;
        }

        
    }
}
