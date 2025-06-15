using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // IOrderDetailBusinessService

    public interface IOrderDetailBusinessService
    {
        OrderDetailDTO GetOrderDetail(out TransactionalInformation transaction, params object[] id);

        IQueryable<OrderDetailDTO> GetAllOrderDetails(out TransactionalInformation transaction);
    }
}