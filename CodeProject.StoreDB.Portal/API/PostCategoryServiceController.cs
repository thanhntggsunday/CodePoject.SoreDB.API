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
    /// Defines the <see cref="PostCategoryServiceController" />
    /// </summary>
    [RoutePrefix("api/PostCategoryService")]
    public class PostCategoryServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _postCategoryBusinessService
        /// </summary>
        private IPostCategoryBusinessService _postCategoryBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCategoryServiceController"/> class.
        /// </summary>
        /// <param name="postCategoryBusinessService">The postCategoryBusinessService<see cref="IPostCategoryBusinessService"/></param>
        public PostCategoryServiceController(IPostCategoryBusinessService postCategoryBusinessService)
        {
            _postCategoryBusinessService = postCategoryBusinessService;
        }

        /// <summary>
        /// The GetPostCategories
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAll")]
        [HttpPost]
        public HttpResponseMessage GetAll(HttpRequestMessage request,
            [FromBody] PostCategoryViewModel postCategoryViewModel)
        {
            var transaction = new TransactionalInformation();

            int currentPageNumber = postCategoryViewModel.Pager.CurrentPageNumber;

            int pageSize = postCategoryViewModel.Pager.PageSize;

            var postCategoryDTOList = _postCategoryBusinessService.GetAllPostCategories(out transaction);

            if (transaction.ReturnStatus == false)
            {
                postCategoryViewModel.ReturnStatus = false;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                postCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postCategoryViewModel);
                return responseError;
            }
            else
            {
                postCategoryViewModel.ReturnStatus = true;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                postCategoryViewModel.PostCategoryDTOs = postCategoryDTOList;
                postCategoryViewModel.Pager = transaction.Pager;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, postCategoryViewModel);
            return response;
        }

        /// <summary>
        /// The CreatePostCategory
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request,
           [FromBody] PostCategoryViewModel postCategoryViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            PostCategory postCategory = new PostCategory();
            HttpResponseMessage response;

            postCategory.CopyFromPostCategoryViewModel(postCategoryViewModel);
            postCategory.CreatedBy = User.Identity.Name;
            postCategory.CreatedDate = DateTime.Now;

            _postCategoryBusinessService.CreatePostCategory(postCategory, out transaction);

            if (transaction.ReturnStatus == false)
            {
                postCategoryViewModel.ReturnStatus = false;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                postCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postCategoryViewModel);
                return responseError;
            }
            else
            {
                postCategoryViewModel.ID = postCategory.ID;
                postCategoryViewModel.ReturnStatus = true;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, postCategoryViewModel);
            }

            return response;
        }

        /// <summary>
        /// The UpdatePostCategory
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request,
          [FromBody] PostCategoryViewModel postCategoryViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            PostCategory postCategory = new PostCategory();
            HttpResponseMessage response;

            postCategory.CopyFromPostCategoryViewModel(postCategoryViewModel);
            postCategory.UpdatedBy = User.Identity.Name;
            postCategory.UpdatedDate = DateTime.Now;

            _postCategoryBusinessService.UpdatePostCategory(postCategory, out transaction);

            if (transaction.ReturnStatus == false)
            {
                postCategoryViewModel.ReturnStatus = false;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;
                postCategoryViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postCategoryViewModel);
                return responseError;
            }
            else
            {
                postCategoryViewModel.ID = postCategory.ID;
                postCategoryViewModel.ReturnStatus = true;
                postCategoryViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, postCategoryViewModel);
            }

            return response;
        }

        /// <summary>
        /// The GetPostCategoryById
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postCategoryViewModel">The postCategoryViewModel<see cref="PostCategoryViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetById")]
        [HttpPost]
        public HttpResponseMessage GetById(HttpRequestMessage request,
          [FromBody] PostCategoryViewModel postCategoryViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var postCategoryId = postCategoryViewModel.ID;
            var categoryDto = _postCategoryBusinessService.GetPostCategory(postCategoryId,
                out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                postCategoryViewModel.ReturnStatus = false;
                postCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                postCategoryViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postCategoryViewModel);
                return responseError;
            }
            else
            {
                postCategoryViewModel.CopyFromPostCategoryDTO(categoryDto);
                postCategoryViewModel.ReturnStatus = true;
                postCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, postCategoryViewModel);
            }

            return response;
        }


        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request,
          [FromBody] PostCategoryViewModel postCategoryViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var postCategoryId = postCategoryViewModel.ID;

            _postCategoryBusinessService.DeletePostCategory(postCategoryId,
                out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                postCategoryViewModel.ReturnStatus = false;
                postCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                postCategoryViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postCategoryViewModel);
                return responseError;
            }
            else
            {
               
                postCategoryViewModel.ReturnStatus = true;
                postCategoryViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, postCategoryViewModel);
            }

            return response;
        }

    }
}
