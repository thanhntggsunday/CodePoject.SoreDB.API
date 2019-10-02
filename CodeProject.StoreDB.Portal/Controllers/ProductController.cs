namespace CodeProject.StoreDB.Portal.Controllers
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.DataService.Logger;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using CodeProject.StoreDB.Portal.Models.BusinessModels;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="ProductController" />
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// Defines the _productBusinessService
        /// </summary>
        private IProductBusinessService _productBusinessService;

        /// <summary>
        /// Defines the _productCategoryBusinessService
        /// </summary>
        private IProductCategoryBusinessService _productCategoryBusinessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productBusinessService">The productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="productCategoryBusinessService">The productCategoryBusinessService<see cref="IProductCategoryBusinessService"/></param>
        public ProductController(IProductBusinessService productBusinessService, 
            IProductCategoryBusinessService productCategoryBusinessService)
        {
            _productBusinessService = productBusinessService;
            _productCategoryBusinessService = productCategoryBusinessService;
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
            int pageSize = 16;
            var productViewModel = new ProductViewModel();

            productViewModel = ProductsBusinessModel.GetAllProductsByPaging(_productBusinessService, page, pageSize);
            ViewBag.MenuItem = "PRODUCT";

            return View(productViewModel);
        }

        // GET: Product/Details/5
        /// <summary>
        /// The Details
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            var productViewModel = new ProductViewModel();
            productViewModel = ProductsBusinessModel.GetProductDetailById(_productBusinessService, id);

            return View(productViewModel);
        }

        /// <summary>
        /// The SearchProductByName
        /// </summary>
        /// <param name="page">The page<see cref="int"/></param>
        /// <param name="productName">The productName<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult SearchProductByName(int page = 1, string productName = "")
        {
            int pageSize = 12;
            var productViewModel = new ProductViewModel();

            productViewModel = ProductsBusinessModel.GetProductsByName(_productBusinessService, productName,
                page, pageSize);

            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult GetProductsByCategoryId(int categoryId = 0, int page = 1)
        {
            int pageSize = 12;
            var productViewModel = new ProductViewModel();

            productViewModel = ProductsBusinessModel.GetProductsByCategoryId(_productBusinessService,
                categoryId, page, pageSize);

            return View(productViewModel);
        }


        [HttpGet]
        public JsonResult GetJsonData(int page = 1, int pageSize = 12, string productName = "")
        {
            // pageSize = 8;
            var productViewModel = new ProductViewModel();

            productViewModel = ProductsBusinessModel.GetProductsByName(_productBusinessService, productName,
                page, pageSize);

            return Json(productViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
