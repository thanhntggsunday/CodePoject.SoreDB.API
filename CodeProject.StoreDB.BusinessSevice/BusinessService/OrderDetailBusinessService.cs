namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using AutoMapper;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="OrderDetailBusinessService" />
    /// </summary>
    public class OrderDetailBusinessService : IOrderDetailBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public OrderDetailBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        public IQueryable<OrderDetailDTO> GetAllOrderDetails(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            IQueryable<OrderDetailDTO> orderDetailDTOs = null;
            
            try
            {
                // Begin session
                _dataService.CreateSession();
                // Todo here:


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
                // End session:
                _dataService.CloseSession();
            }

            return orderDetailDTOs;
        }

        /// <summary>
        /// The GetOrderDetail
        /// </summary>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <param name="id">The id<see cref="object[]"/></param>
        /// <returns>The <see cref="OrderDetailDTO"/></returns>
        public OrderDetailDTO GetOrderDetail(out TransactionalInformation transaction, params object[] id)
        {
            transaction = new TransactionalInformation();

            OrderDetail orderDetail = new OrderDetail();

            try
            {
                _dataService.CreateSession();

                // Todo here:

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

            var orderDetailDto = Mapper.Map<OrderDetail, OrderDetailDTO>(orderDetail);

            return orderDetailDto;
        }
    }
}