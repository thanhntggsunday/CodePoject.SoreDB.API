namespace CodeProject.StoreDB.Portal.Controllers
{
    using CodeProject.Interfaces.BLL;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using CodeProject.StoreDB.Portal.Models.BusinessModels;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="HomeController" />
    /// </summary>
    public class HomeController : Controller
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
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="productBusinessService">The productBusinessService<see cref="IProductBusinessService"/></param>
        /// <param name="productCategoryBusinessService">The productCategoryBusinessService<see cref="IProductCategoryBusinessService"/></param>
        public HomeController(IProductBusinessService productBusinessService, IProductCategoryBusinessService productCategoryBusinessService)
        {
            this._productBusinessService = productBusinessService;
            this._productCategoryBusinessService = productCategoryBusinessService;
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Index()
        {
            var productViewModel = new ProductViewModel();
            productViewModel = ProductsBusinessModel.GetNewProducts(_productBusinessService, 8);

            List<Slide> slides = new List<Slide>();

            slides.Add(new Slide { ID = 1, Image = "/asset/client/images/slide-1-image.png", Status = true });
            slides.Add(new Slide { ID = 2, Image = "/asset/client/images/slide-2-image.jpg", Status = true });
            slides.Add(new Slide { ID = 3, Image = "/asset/client/images/slide-3-image.jpg", Status = true });

            ViewBag.Slides = slides;
            ViewBag.MenuItem = "HOME";

            return View(productViewModel);
        }

        /// <summary>
        /// The ProductCategory
        /// </summary>
        /// <returns>The <see cref="PartialViewResult"/></returns>
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = _productCategoryBusinessService.GetAll(out TransactionalInformation transactionalInformation);
            return PartialView(model);
        }

        /// <summary>
        /// The About
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MenuItem = "ABOUT";

            return View();
        }

        /// <summary>
        /// The Contact
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.MenuItem = "CONTACT";

            return View();
        }

        public ActionResult Delivery()
        {
            ViewBag.Message = "Delivery page.";
            ViewBag.MenuItem = "DELIVERY";

            return View();
        }
    }
}
