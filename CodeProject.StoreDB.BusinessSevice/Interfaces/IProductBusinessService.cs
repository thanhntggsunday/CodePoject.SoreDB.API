using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Interfaces.BLL;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CodeProject.Interfaces.BLL
{
    public interface IProductBusinessService : IBusinessServiceBase
    {
        Product CreateProduct(Product product, out TransactionalInformation transaction);

        void UpdateProduct(Product product, out TransactionalInformation transaction);

        ProductDTO GetProduct(object productID, out TransactionalInformation transaction);

        void DeleteProduct(object id, out TransactionalInformation transaction);

        List<ProductDTO> GetAllProductsByPaging(int currentPageNumber, int pageSize,
            out TransactionalInformation transaction);

        List<ProductDTO> GetProductsByCategoryId(int categoryId, int currentPageNumber,
            int pageSize, out TransactionalInformation transaction);

        List<ProductDTO> GetProductByProductNameAndCategoryName(string categoryName, string productName,
            int currentPageNumber, int pageSize, out TransactionalInformation transaction);

        void GetNewProducts(int productNumber, out List<ProductDTO> newLaptops, out List<ProductDTO> newSmartPhones,
            out TransactionalInformation transaction);
        void GetProductDetailsAndProductsRelate(object productID, out ProductDTO productDto,
          out List<ProductDTO> productDTOs, out TransactionalInformation transaction);

        List<ProductDTO> GetProductsByName(out TransactionalInformation transaction,
           int currentPageNumber = 1, int pageSize = 10, string productName = "");
    }
}