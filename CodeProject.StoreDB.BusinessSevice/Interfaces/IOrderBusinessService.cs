using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // IOderBusinessService
    public interface IOrderBusinessService
    {
        Order CreateOrder(Order obj, string userName, out TransactionalInformation transaction);

        void UpdateOrder(Order obj, out TransactionalInformation transaction);

        OrderDTO GetOrder(object id, out TransactionalInformation transaction, out List<OrderDetailDTO> orderDetailDTOs);

        // void DeleteOrder(object id, out TransactionalInformation transaction);

        List<OrderDTO> GetOrdersByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);

        
    }
}