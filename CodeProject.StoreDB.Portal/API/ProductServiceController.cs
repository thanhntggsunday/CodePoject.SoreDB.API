namespace CodeProject.StoreDB.Portal.API
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.Utilities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="ProductServiceController" />
    /// </summary>
    [RoutePrefix("api/ProductService")]
    [Authorize]
    public class ProductServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _productBusinessService
        /// </summary>
        private IProductBusinessService _productBusinessService;

        // private IProductCategoryBusinessService _productCategoryBusinessService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceController"/> class.
        /// </summary>
        /// <param name="productBusinessService">The productBusinessService<see cref="IProductBusinessService"/></param>
        public ProductServiceController(IProductBusinessService productBusinessService)
        {
            this._productBusinessService = productBusinessService;
        }

        // api/ProductService/GetProducts
        /// <summary>
        /// The GetProducts
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAllPaging")]
        [HttpPost]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request,
            [FromBody] ProductViewModel productViewModel)
        {
            var transaction = new TransactionalInformation();

            int currentPageNumber = productViewModel.Pager.CurrentPageNumber;

            int pageSize = productViewModel.Pager.PageSize;

            var productDtoList = _productBusinessService
                                    .GetAllProductsByPaging(currentPageNumber, pageSize, out transaction);

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ProductDTOs = productDtoList;
                productViewModel.Pager = transaction.Pager;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, productViewModel);
            return response;
        }

        /// <summary>
        /// The CreateProduct
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            Product product = new Product();
            HttpResponseMessage response;

            product.CopyFromProductViewModel(productViewModel);

            product.CreatedBy = User.Identity.Name;
            product.CreatedDate = DateTime.Now;

            _productBusinessService.CreateProduct(product, out transaction);

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }
            else
            {
                productViewModel.ID = product.ID;
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, productViewModel);
            }

            return response;
        }

        /// <summary>
        /// The UpdateProduct
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            Product product = new Product();
            HttpResponseMessage response;

            product.CopyFromProductViewModel(productViewModel);

            product.UpdatedBy = User.Identity.Name;
            product.UpdatedDate = DateTime.Now;

            _productBusinessService.UpdateProduct(product, out transaction);

            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }
            else
            {
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, productViewModel);
            }

            return response;
        }

        /// <summary>
        /// The GetProductById
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="productViewModel">The productViewModel<see cref="ProductViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetById")]
        [HttpPost]
        public HttpResponseMessage GetById(HttpRequestMessage request,
            [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var productId = productViewModel.ID;
            var productDto = _productBusinessService.GetProduct(productId, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                productViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }
            else
            {
                productViewModel.CopyFromProductDTO(productDto);

                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, productViewModel);
            }

            return response;
        }

        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request,
           [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            // var productId = productViewModel.ID;

            _productBusinessService.DeleteProduct(productViewModel.ID, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                productViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }
            else
            {
               
                productViewModel.ReturnStatus = true;
                productViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, productViewModel);
            }

            return response;
        }

    }
}
