namespace CodeProject.StoreDB.Portal.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Defines the <see cref="ErrorController" />
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// The Unauthorised
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Unauthorised()
        {
            return View();
        }
    }
}
