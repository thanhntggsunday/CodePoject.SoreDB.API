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
    /// Defines the <see cref="PostServiceController" />
    /// </summary>
    [RoutePrefix("api/PostService")]
    public class PostServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _postBusinessService
        /// </summary>
        private IPostBusinessService _postBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostServiceController"/> class.
        /// </summary>
        /// <param name="postBusinessService">The postBusinessService<see cref="IPostBusinessService"/></param>
        public PostServiceController(IPostBusinessService postBusinessService)
        {
            _postBusinessService = postBusinessService;
        }

        /// <summary>
        /// The GetPostCategories
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAllPaging")]
        [HttpPost]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request,
           [FromBody] PostViewModel postViewModel)
        {
            var transaction = new TransactionalInformation();

            int currentPageNumber = postViewModel.Pager.CurrentPageNumber;

            int pageSize = postViewModel.Pager.PageSize;

            var postDTOList = _postBusinessService.GetPostsByPaging(currentPageNumber,
                                pageSize, out transaction);

            if (transaction.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postViewModel);
                return responseError;
            }
            else
            {
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.PostDTOs = postDTOList;
                postViewModel.Pager = transaction.Pager;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, postViewModel);
            return response;
        }

        /// <summary>
        /// The CreatePost
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request,
          [FromBody] PostViewModel postViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            Post post = new Post();
            HttpResponseMessage response;

            post.CopyFromPostViewModel(postViewModel);
            post.CreatedBy = User.Identity.Name;
            post.CreatedDate = DateTime.Now;

            _postBusinessService.CreatePost(post, out transaction);

            if (transaction.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postViewModel);
                return responseError;
            }
            else
            {
                postViewModel.ID = post.ID;
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, postViewModel);
            }

            return response;
        }

        /// <summary>
        /// The UpdatePost
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request,
         [FromBody] PostViewModel postViewModel)
        {
            // TODO: Add insert logic here

            TransactionalInformation transaction;
            Post post = new Post();
            HttpResponseMessage response;

            post.CopyFromPostViewModel(postViewModel);
            post.UpdatedBy = User.Identity.Name;
            post.UpdatedDate = DateTime.Now;

            _postBusinessService.UpdatePost(post, out transaction);

            if (transaction.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postViewModel);
                return responseError;
            }
            else
            {
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transaction.ReturnMessage;

                response = request.CreateResponse(HttpStatusCode.OK, postViewModel);
            }

            return response;
        }

        /// <summary>
        /// The GetPostById
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="postViewModel">The postViewModel<see cref="PostViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetById")]
        [HttpPost]
        public HttpResponseMessage GetById(HttpRequestMessage request,
            [FromBody] PostViewModel postViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var postId = postViewModel.ID;
            var postDto = _postBusinessService.GetPost(postId, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                postViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postViewModel);
                return responseError;
            }
            else
            {
                postViewModel.CopyFromPostDTO(postDto);
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, postViewModel);
            }

            return response;
        }

        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request,
            [FromBody] PostViewModel postViewModel)
        {
            TransactionalInformation transactionalInformation;
            HttpResponseMessage response;
            var postId = postViewModel.ID;

            _postBusinessService.DeletePost(postId, out transactionalInformation);

            if (transactionalInformation.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                postViewModel.ValidationErrors = transactionalInformation.ValidationErrors;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, postViewModel);
                return responseError;
            }
            else
            {
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transactionalInformation.ReturnMessage;
                response = request.CreateResponse(HttpStatusCode.OK, postViewModel);
            }

            return response;
        }
    }
}
