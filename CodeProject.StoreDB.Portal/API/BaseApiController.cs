namespace CodeProject.StoreDB.Portal.API
{
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="BaseApiController" />
    /// </summary>
    [Authorize(Roles = "Admin, Manager")]
    public class BaseApiController : ApiController
    {
    }
}
