namespace CodeProject.StoreDB.Portal.API
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.Utilities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="ProductCategoryServiceController" />
    /// </summary>
    [RoutePrefix("api/ProductCategoryService")]
    public class ProductCategoryServiceController : BaseApiController
    {
        // private IProductBusinessService _productBusinessService;
        /// <summary>
        /// Defines the _productCategoryBusinessService
        /// </summary>
        private IProductCategoryBusinessService _productCategoryBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryServiceController"/> class.
        /// </summary>
        /// <param name="productCategoryBusinessService">The productCategoryBusinessService<see cref="IProductCategoryBusinessService"/></param>
        public ProductCategoryServiceController(IProductCategoryBusinessService productCategoryBusinessService)
        {
            _productCategoryBusinessService = productCategoryBusinessService;
        }

        /// <summary>
        /// The GetProductCategories
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAll")]
        [HttpPost]
        public HttpResponseMessage GetAll(HttpRequestMessage request,
            [FromBody] ProductCategoryViewModel productCategoryViewModel)
        {
            var transaction = new TransactionalInformation();  
            var productCategoryDtoList = _productCategoryBusinessService.GetAll(out transaction);

            if (transaction.ReturnStatus == false)
            {
                productCategoryViewModel.ReturnStatus = false;
                productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                productCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                return responseError;
            }
            else
            {
                productCategoryViewModel.ReturnStatus = true;
                productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                productCategoryViewModel.ProductCategoryDTOs = productCategoryDtoList;
                productCategoryViewModel.Pager = transaction.Pager;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
            return response;
        }

        /// <summary>
        /// The CreateProductCategory
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request,
            [FromBody] ProductCategoryViewModel productCategoryViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            ProductCategory productCategory = new ProductCategory();
            HttpResponseMessage response;

            productCategory.CopyFromProductCategoryViewModel(productCategoryViewModel);

            productCategory.CreatedBy = User.Identity.Name;
            productCategory.CreatedDate = DateTime.Now;

            _productCategoryBusinessService.CreateProductCategory(productCategory, out transaction);

            if (transaction.ReturnStatus == false)
            {
                productCategoryViewModel.ReturnStatus = false;
                productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                productCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                return responseError;
            }
            else
            {
                productCategoryViewModel.ID = productCategory.ID;
                productCategoryViewModel.ReturnStatus = true;
                productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
            }

            return response;
        }

        /// <summary>
        /// The UpdateProductCategory
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request,
            [FromBody] ProductCategoryViewModel productCategoryViewModel)
        {
            {
                // TODO: Add insert logic here

                TransactionalInformation transaction;
                ProductCategory productCategory = new ProductCategory();
                HttpResponseMessage response;

                productCategory.CopyFromProductCategoryViewModel(productCategoryViewModel);

                productCategory.UpdatedBy = User.Identity.Name;
                productCategory.UpdatedDate = DateTime.Now;

                _productCategoryBusinessService.UpdateProductCategory(productCategory, out transaction);

                if (transaction.ReturnStatus == false)
                {
                    productCategoryViewModel.ReturnStatus = false;
                    productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                    productCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                    var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                    return responseError;
                }
                else
                {
                    productCategoryViewModel.ReturnStatus = true;
                    productCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                    response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
                }

                return response;
            }
        }

        /// <summary>
        /// The GetProductById
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productCategoryViewModel">The productCategoryViewModel<see cref="ProductCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetById")]
        [HttpPost]
        public HttpResponseMessage GetById(HttpRequestMessage request,
           [FromBody] ProductCategoryViewModel productCategoryViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var productCategoryId = productCategoryViewModel.ID;
            var categoryDto = _productCategoryBusinessService.GetProductCategory(productCategoryId,
                out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                productCategoryViewModel.ReturnStatus = false;
                productCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                productCategoryViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                return responseError;
            }
            else
            {
                productCategoryViewModel.CopyFromProductCategoryDTO(categoryDto);
                productCategoryViewModel.ReturnStatus = true;
                productCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
            }

            return response;
        }

        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request,
              [FromBody] ProductCategoryViewModel productCategoryViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var id = productCategoryViewModel.ID;
         
            _productCategoryBusinessService.DeleteProductCategory(id, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                productCategoryViewModel.ReturnStatus = false;
                productCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                productCategoryViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                return responseError;
            }
            else
            {               
                productCategoryViewModel.ReturnStatus = true;
                productCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
            }

            return response;
        }

    }
}
