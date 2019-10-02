namespace CodeProject.StoreDB.Portal.Models.BusinessModels
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Portal.Classes.Utilities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ProductsBusinessModel" />
    /// </summary>
    public static class ProductsBusinessModel
    {
        /// <summary>
        /// The GetProductsByPaging
        /// </summary>
        /// <param name="_productBusinessService">The _productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="page">The page<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="ProductViewModel"/></returns>
        public static ProductViewModel GetAllProductsByPaging(IProductBusinessService _productBusinessService, int page, int pageSize)
        {
            var transaction = new TransactionalInformation();
            // var productDtoList = _productBusinessService.GetProductsByPaging(page, pageSize, out transaction);

            var products = _productBusinessService.GetAllProductsByPaging(page, pageSize, out transaction);

            var productViewModel = new ProductViewModel();

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                //var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                //return responseError;
            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ProductDTOs = products;
                productViewModel.Pager = transaction.Pager;
            }

            return productViewModel;
        }

        /// <summary>
        /// The GetNewProducts
        /// </summary>
        /// <param name="_productBusinessService">The _productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="topNumber">The topNumber<see cref="int"/></param>
        /// <returns>The <see cref="ProductViewModel"/></returns>
        public static ProductViewModel GetNewProducts(IProductBusinessService _productBusinessService, int topNumber)
        {
            var transaction = new TransactionalInformation();
            var newSmartPhones = new List<ProductDTO>();
            var newLaptops = new List<ProductDTO>();

            _productBusinessService.GetNewProducts(topNumber, out newLaptops, out newSmartPhones, out transaction);

            var productViewModel = new ProductViewModel();

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;
            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.Pager = transaction.Pager;

                productViewModel.LaptopDTOs = newLaptops;
                productViewModel.SmartPhonesDTOs = newSmartPhones;
            }

            return productViewModel;
        }

        /// <summary>
        /// The GetProductDetailById
        /// </summary>
        /// <param name="_productBusinessService">The _productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ProductViewModel"/></returns>
        public static ProductViewModel GetProductDetailById(IProductBusinessService _productBusinessService, int id)
        {
            TransactionalInformation transactionalInformation;
            ProductViewModel productViewModel = new ProductViewModel();
            ProductDTO productDto;
            List<ProductDTO> productRelatedDto = new List<ProductDTO>();

            _productBusinessService.GetProductDetailsAndProductsRelate(id,
              out productDto, out productRelatedDto, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                productViewModel.ValidationErrors = transactionalInformation.ValidationErrors;
            }
            else
            {
                productViewModel.CopyFromProductDTO(productDto);
                productViewModel.ProductDTOs = productRelatedDto;

                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
            }

            return productViewModel;
        }

        /// <summary>
        /// The SearchProductByName
        /// </summary>
        /// <param name="_productBusinessService">The _productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="productName">The productName<see cref="string"/></param>
        /// <param name="page">The page<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="ProductViewModel"/></returns>
        public static ProductViewModel GetProductsByName(IProductBusinessService _productBusinessService,
            string productName, int page, int pageSize)
        {
            var transaction = new TransactionalInformation();
           
            var sqlProductName = "%" + productName + "%";
            var productDtoList = _productBusinessService.GetProductsByName(out transaction, 
                                    page, pageSize, sqlProductName);
            var productViewModel = new ProductViewModel();

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ProductDTOs = productDtoList;
                productViewModel.Pager = transaction.Pager;

                productViewModel.ProductSearchContent = new ProductSearchContent();
                productViewModel.ProductSearchContent.ProductName = productName;
                productViewModel.ProductSearchContent.Url = "/Product/SearchProductByName";
            }

            return productViewModel;
        }

        public static ProductViewModel GetProductsByCategoryId(IProductBusinessService _productBusinessService,
            int categoryId, int page, int pageSize)
        {
            var transaction = new TransactionalInformation();
            var productDtoList = _productBusinessService
                .GetProductsByCategoryId(categoryId, page, pageSize, out transaction);

            var productViewModel = new ProductViewModel();
            string categoryName = "";

            if (productDtoList != null)
            {
                if (productDtoList.Count > 0)
                {
                    categoryName = productDtoList[0].CategoryName;
                }
            }

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ProductDTOs = productDtoList;
                productViewModel.Pager = transaction.Pager;

                productViewModel.ProductSearchContent = new ProductSearchContent();
                productViewModel.ProductSearchContent.CategoryId = categoryId;
                productViewModel.ProductSearchContent.CategoryName = categoryName;
                productViewModel.ProductSearchContent.Url = "/Product/GetProductsByCategoryId";
            }

            return productViewModel;
        }

        


    }
}
