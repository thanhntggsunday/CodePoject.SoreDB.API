namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using AutoMapper;
    using CodeProject.Business.Common.Errors;
    using CodeProject.Business.Common.Utility;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessRules;
    using CodeProject.StoreDB.BusinessSevice.BusinessServiceModels;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.DataService.SQL;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using Dapper;
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="OderBusinessService" />
    /// </summary>
    public class OderBusinessService : IOrderBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OderBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public OderBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The CreateOrder
        /// </summary>
        /// <param name="order">The obj<see cref="Order"/></param>
        /// <param name="userName">The userName<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="Order"/></returns>
        public Order CreateOrder(Order order, string userName, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            ShoppingCartBusinessService shoppingCartBusinessService = ShoppingCartBusinessService.GetCart(userName);

            try
            {
                OrderBusinessRules orderBusinessRules = new OrderBusinessRules();
                ValidationResult results = orderBusinessRules.Validate(order);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return order;
                }

                _dataService.CreateSession();

                var insertParam = new DynamicParameters();
                insertParam.Add("@CustomerName", order.CustomerName);
                insertParam.Add("@CreatedBy", order.CreatedBy);
                insertParam.Add("@CreatedDate", order.CreatedDate);
                insertParam.Add("@CustomerAddress", order.CustomerAddress);
                insertParam.Add("@CustomerEmail", order.CustomerEmail);
                insertParam.Add("@CustomerMessage", order.CustomerMessage);
                insertParam.Add("@CustomerMobile", order.CustomerMobile);
                insertParam.Add("@PaymentMethod", order.PaymentMethod);
                insertParam.Add("@PaymentStatus", order.PaymentStatus);
                insertParam.Add("@Status", order.Status);
                insertParam.Add("@Total", order.Total);
                insertParam.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                _dataService.OrderRepository.Insert(OrderScript.Insert, insertParam, CommandType.Text);

                order.ID = insertParam.Get<int>("@ID");

                CreateOrderDetail(order, shoppingCartBusinessService);


                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfull");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

            return order;
        }

        /// <summary>
        /// The CreateOrderDetail
        /// </summary>
        /// <param name="order">The order<see cref="Order"/></param>
        /// <param name="shoppingCartBusinessService">The shoppingCartBusinessService<see cref="ShoppingCartBusinessService"/></param>
        /// <returns>The <see cref="int"/></returns>
        private int CreateOrderDetail(Order order, ShoppingCartBusinessService shoppingCartBusinessService)
        {
            var cartItems = shoppingCartBusinessService.GetCartItems();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = order.ID,
                    ProductID = (int)item.ProductId,
                    Quantitty = item.Count
                };
                // Set the order total of the shopping cart
                var insertOrderDetailsParam = new DynamicParameters();
                insertOrderDetailsParam.Add("@OrderID", order.ID);
                insertOrderDetailsParam.Add("@ProductId", (int) item.ProductId);
                insertOrderDetailsParam.Add("@Quantitty", item.Count);
               
                _dataService.OrderDetailRepository.Insert(OrderDetailsScript.Insert, 
                    insertOrderDetailsParam, CommandType.Text);
            }
            // Set the order's total to the orderTotal count
            order.Total = shoppingCartBusinessService.GetTotalPrice();

            var insertParam = new DynamicParameters();          
            
            insertParam.Add("@Total", order.Total);
            insertParam.Add("@ID", order.ID);

            _dataService.OrderRepository.Update(OrderScript.UpdateTotal, insertParam, CommandType.Text);
            
            // Save the order
            _dataService.CommitTransaction(true);
            // Empty the shopping cart
            shoppingCartBusinessService.EmptyCart();
            // Return the OrderId as the confirmation number
            return order.ID;
        }

        

        /// <summary>
        /// The GetOrder
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="OrderDTO"/></returns>
        public OrderDTO GetOrder(object id, out TransactionalInformation transaction,
            out List<OrderDetailDTO> orderDetailDTOs)
        {
            transaction = new TransactionalInformation();
            orderDetailDTOs = new List<OrderDetailDTO>();
           
            OrderDTO orderDto = new OrderDTO();

            try
            {
                _dataService.CreateSession();
                // Todo here:
                var param = new DynamicParameters();

                param.Add("@ID", id);

                orderDto = _dataService.OrderRepository.GetById(OrderScript.GetById, param, CommandType.Text);

                int orderId = (int)id;
                var orderDetailParam = new DynamicParameters();
                orderDetailParam.Add("@OrderID", orderId);

                var oderDetailsList = _dataService.OrderDetailRepository
                                        .GetManyRows(OrderDetailsScript.GetManyByOrderId,
                                            orderDetailParam, CommandType.Text);

                foreach (var item in oderDetailsList)
                {
                    orderDetailDTOs.Add(new OrderDetailDTO
                    {
                        OrderID = item.OrderID,
                        ProductID = item.ProductID,
                        ProductName = item.ProductName,
                        Quantitty = item.Quantitty
                    });
                }


                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

           

            return orderDto;
        }

        /// <summary>
        /// The GetOrdersByPaging
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/></param>
        /// <param name="sortDirection">The sortDirection<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{OrderDTO}"/></returns>
        public List<OrderDTO> GetOrdersByPaging(int currentPageNumber, int pageSize, string sortExpression,
                                        string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

           
            List<OrderDTO> ordersDtoList = new List<OrderDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageIndex", currentPageNumber);
                parameters.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ordersDtoList = _dataService.OrderRepository
                                        .GetManyRows(OrderScript.GetAllPaging,
                                            parameters, CommandType.Text);
                totalRows = parameters.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows, 
                                pageSize, currentPageNumber);

                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

            

            return ordersDtoList;
        }

        /// <summary>
        /// The UpdateOrder
        /// </summary>
        /// <param name="obj">The obj<see cref="Order"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void UpdateOrder(Order obj, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                OrderBusinessRules orderBusinessRules = new OrderBusinessRules();
                ValidationResult results = orderBusinessRules.Validate(obj);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _dataService.CreateSession();

                // Todo here:
                var parameters = new DynamicParameters();
                parameters.Add("@PaymentStatus", obj.PaymentStatus);
                parameters.Add("@Status", obj.Status);
                parameters.Add("@ID", obj.ID);

                _dataService.OrderRepository.Update(OrderScript.Update,
                                            parameters, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        
        
    }
}