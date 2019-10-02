namespace CodeProject.StoreDB.Portal.API
{
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="UserRolesServiceController" />
    /// </summary>
    [RoutePrefix("api/UserRolesService")]
    public class UserRolesServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Gets or sets the UserManager
        /// </summary>
        private UserManager<ApplicationUser> UserManager { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRolesServiceController"/> class.
        /// </summary>
        public UserRolesServiceController()
        {
            _context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        /// <summary>
        /// The GetRoles
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="userRolesViewModels">The userRolesViewModels<see cref="UserRolesViewModels"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("GetAllUserRoles")]
        [HttpPost]
        public HttpResponseMessage GetAllUserRoles(HttpRequestMessage request,
          [FromBody] UserRolesViewModels userRolesViewModels)
        {
            try
            {
                // userRolesViewModels.userRolesList = new List<UserRolesDTO>();
                //duyệt user
                foreach (var u in _context.Users)
                {
                    //duyệt các role of user
                    foreach (var r in u.Roles)
                    {
                        string roleName = "";
                        var role = _context.Roles.FirstOrDefault(o => o.Id == r.RoleId);
                        roleName = role.Name;
                        //thêm thông tin role of user vào danh sách
                        userRolesViewModels.UserRolesList.Add(new UserRolesDTO()
                        {
                            UserId = u.Id,
                            // UserName = u.UserName,
                            Email = u.Email,
                            RoleName = roleName
                        });
                    }
                }

                userRolesViewModels.ReturnStatus = true;
                userRolesViewModels.ReturnMessage = "Get Successfuly!";
            }
            catch (Exception ex)
            {
                userRolesViewModels.ReturnStatus = false;
                userRolesViewModels.ReturnMessage = ex.Message;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, userRolesViewModels);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, userRolesViewModels);
            return response;
        }

        [Route("GetAllUsersAndRolesList")]
        [HttpPost]
        public HttpResponseMessage GetAllUsersAndRolesList(HttpRequestMessage request,
          [FromBody] UserRolesViewModels userRolesViewModels)
        {
            try
            {
                userRolesViewModels.Users = _context.Users.ToList();
                userRolesViewModels.Roles = _context.Roles.ToList();
                userRolesViewModels.ReturnStatus = true;
                userRolesViewModels.ReturnMessage = "Get Successfuly!";
            }
            catch (Exception ex)
            {
                userRolesViewModels.ReturnStatus = false;
                userRolesViewModels.ReturnMessage = ex.Message;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, userRolesViewModels);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, userRolesViewModels);
            return response;
        }

        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="userRolesViewModels">The userRolesViewModels<see cref="UserRolesViewModels"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("CreateUserRole")]
        [HttpPost]
        public HttpResponseMessage CreateUserRole(HttpRequestMessage request,
              [FromBody] UserRolesViewModels userRolesViewModels)
        {
            try
            {
                var result = UserManager.AddToRole(userRolesViewModels.UserId, userRolesViewModels.RoleName);

                userRolesViewModels.ReturnStatus = true;
                userRolesViewModels.ReturnMessage = "Inserted Successfuly!";
            }
            catch (Exception ex)
            {
                userRolesViewModels.ReturnStatus = false;
                userRolesViewModels.ReturnMessage = ex.Message;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, userRolesViewModels);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, userRolesViewModels);
            return response;
        }

        /// <summary>
        /// The DeleteRole
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="userRolesViewModels">The userRolesViewModels<see cref="UserRolesViewModels"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("DeleteUserRole")]
        [HttpPost]
        public HttpResponseMessage DeleteUserRole(HttpRequestMessage request,
              [FromBody] UserRolesViewModels userRolesViewModels)
        {
            try
            {
                //xóa user từ role
                var result = UserManager.RemoveFromRole(userRolesViewModels.UserId, userRolesViewModels.RoleName);

                userRolesViewModels.ReturnStatus = true;
                userRolesViewModels.ReturnMessage = "Deleted Successfuly!";
            }
            catch (Exception ex)
            {
                userRolesViewModels.ReturnStatus = false;
                userRolesViewModels.ReturnMessage = ex.Message;

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, userRolesViewModels);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, userRolesViewModels);
            return response;
        }
    }
}
