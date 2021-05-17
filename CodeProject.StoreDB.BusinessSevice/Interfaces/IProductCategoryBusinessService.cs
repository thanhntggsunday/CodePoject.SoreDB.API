using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // IProductCategoryBusinessService
    public interface IProductCategoryBusinessService
    {
        ProductCategory CreateProductCategory(ProductCategory obj, out TransactionalInformation transaction);

        void UpdateProductCategory(ProductCategory obj, out TransactionalInformation transaction);

        ProductCategoryDTO GetProductCategory(object id, out TransactionalInformation transaction);

        void DeleteProductCategory(object id, out TransactionalInformation transaction);

        List<ProductCategoryDTO> GetProductCategorysByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);

        List<ProductCategoryDTO> GetAll(out TransactionalInformation transaction);
    }
}