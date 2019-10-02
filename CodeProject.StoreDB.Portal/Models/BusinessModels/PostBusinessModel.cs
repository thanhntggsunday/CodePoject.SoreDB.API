namespace CodeProject.StoreDB.Portal.Models.BusinessModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;

    /// <summary>
    /// Defines the <see cref="PostBusinessModel" />
    /// </summary>
    public static class PostBusinessModel
    {
        /// <summary>
        /// The GetPostsByPaging
        /// </summary>
        /// <param name="_postBusinessService">The _postBusinessService<see cref="IPostBusinessService"/></param>
        /// <param name="page">The page<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <returns>The <see cref="PostViewModel"/></returns>
        public static PostViewModel GetPostsByPaging(IPostBusinessService _postBusinessService, int page, int pageSize)
        {
            var transaction = new TransactionalInformation();
            var posts = _postBusinessService
                                 .GetPostsByPaging(page, pageSize, out transaction);
            var postViewModel = new PostViewModel();

            if (transaction.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.ValidationErrors = transaction.ValidationErrors;

                //var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                //return responseError;
            }
            else
            {
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.PostDTOs = posts;
                postViewModel.Pager = transaction.Pager;
            }

            return postViewModel;
        }

        /// <summary>
        /// The GetPostDetail
        /// </summary>
        /// <param name="_postBusinessService">The _postBusinessService<see cref="IPostBusinessService"/></param>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="PostViewModel"/></returns>
        public static PostViewModel GetPostDetail(IPostBusinessService _postBusinessService, int id)
        {
            var transaction = new TransactionalInformation();
            var posts = _postBusinessService
                                 .GetPost(id, out transaction);
            var postViewModel = new PostViewModel();

            if (transaction.ReturnStatus == false)
            {
                postViewModel.ReturnStatus = false;
                postViewModel.ReturnMessage = transaction.ReturnMessage;
                postViewModel.ValidationErrors = transaction.ValidationErrors;

                //var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productCategoryViewModel);
                //return responseError;
            }
            else
            {
                postViewModel.ReturnStatus = true;
                postViewModel.ReturnMessage = transaction.ReturnMessage;

                postViewModel.Alias = posts.Alias;
                postViewModel.CategoryName = posts.CategoryName;
                postViewModel.Content = posts.Content;
                postViewModel.Description = posts.Description;
                postViewModel.CreatedBy = posts.CreatedBy;
                postViewModel.CreatedDate = posts.CreatedDate;
                postViewModel.UpdatedBy = posts.UpdatedBy;
                postViewModel.UpdatedDate = posts.UpdatedDate;
                postViewModel.MetaDescription = posts.MetaDescription;
                postViewModel.MetaKeyword = posts.MetaKeyword;
                postViewModel.Image = posts.Image;

                postViewModel.Pager = transaction.Pager;
            }

            return postViewModel;
        }
    }
}
