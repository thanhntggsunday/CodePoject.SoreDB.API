namespace CodeProject.StoreDB.Portal.Controllers
{
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using CodeProject.StoreDB.Portal.Models.BusinessModels;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="PostController" />
    /// </summary>
    public class PostController : Controller
    {
        /// <summary>
        /// Defines the _postBusinessService
        /// </summary>
        private IPostBusinessService _postBusinessService;

        /// <summary>
        /// Defines the _postCategoryBusinessService
        /// </summary>
        private IPostCategoryBusinessService _postCategoryBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="postBusinessService">The postBusinessService<see cref="IPostBusinessService"/></param>
        /// <param name="postCategoryBusinessService">The postCategoryBusinessService<see cref="IPostCategoryBusinessService"/></param>
        public PostController(IPostBusinessService postBusinessService, IPostCategoryBusinessService postCategoryBusinessService)
        {
            _postBusinessService = postBusinessService;
            _postCategoryBusinessService = postCategoryBusinessService;
        }

        // GET: Product
        /// <summary>
        /// The Index
        /// </summary>
        /// <param name="page">The page<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            int pageSize = 12;
            var postViewModel = new PostViewModel();

            postViewModel = PostBusinessModel.GetPostsByPaging(_postBusinessService, page, pageSize);
            ViewBag.MenuItem = "NEWS";

            return View(postViewModel);
        }

        // GET: Post/Details/5
        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Details(int id)
        {
            var postViewModel = new PostViewModel();

            postViewModel = PostBusinessModel.GetPostDetail(_postBusinessService, id);

            return View(postViewModel);
        }
    }
}
