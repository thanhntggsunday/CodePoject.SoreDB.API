namespace CodeProject.StoreDB.Portal.API
{
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using CodeProject.StoreDB.Portal.Classes.ViewModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="RoleServiceController" />
    /// </summary>
    [RoutePrefix("api/RoleService")]
    // [Authorize(Roles ="Admin")]
    public class RoleServiceController : BaseApiController
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleServiceController"/> class.
        /// </summary>
        public RoleServiceController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="roleViewModel">The roleViewModel<see cref="RoleViewModel"/></param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpPost]
        public HttpResponseMessage GetAll(HttpRequestMessage request,
          [FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var Roles = _context.Roles.ToList();

                var roleDtoList = new List<RoleDTO>();

                foreach (var item in Roles)
                {
                    roleDtoList.Add(new RoleDTO()
                    {
                        RoleName = item.Name,
                        RoleId = item.Id
                    });
                }

                roleViewModel.Roles = roleDtoList;
                roleViewModel.ReturnStatus = true;
                roleViewModel.ReturnMessage.Add("Get Successfuly!");
            }
            catch (Exception ex)
            {
                roleViewModel.ReturnStatus = false;
                roleViewModel.ReturnMessage.Add(ex.Message);

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, roleViewModel);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, roleViewModel);
            return response;
        }


        [Route("GetAllPaging")]
        [HttpPost]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request,
        [FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var countSkip = roleViewModel.Pager.PageSize * (roleViewModel.Pager.CurrentPageNumber - 1);
                var Roles = _context.Roles.AsQueryable().OrderBy(o => o.Id)
                    .Skip(countSkip).Take(roleViewModel.Pager.PageSize);

                var roleDtoList = new List<RoleDTO>();

                foreach (var item in Roles)
                {
                    roleDtoList.Add(new RoleDTO()
                    {
                        RoleName = item.Name,
                        RoleId = item.Id
                    });
                }

                roleViewModel.Roles = roleDtoList;
                roleViewModel.ReturnStatus = true;
                roleViewModel.Pager.TotalRows = _context.Roles.AsQueryable().Count();
                roleViewModel.ReturnMessage.Add("Get Successfuly!");
            }
            catch (Exception ex)
            {
                roleViewModel.ReturnStatus = false;
                roleViewModel.ReturnMessage.Add(ex.Message);

                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, roleViewModel);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, roleViewModel);
            return response;
        }


        /// <summary>
        /// The CreateRole
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="roleViewModel">The roleViewModel<see cref="RoleViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request,
              [FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var role = new IdentityRole();
                role.Name = roleViewModel.RoleName;

                _context.Roles.Add(role);
                _context.SaveChanges();

                roleViewModel.ReturnStatus = true;
                roleViewModel.ReturnMessage.Add("Created Successfully!");
            }
            catch (Exception ex)
            {
                roleViewModel.ReturnStatus = false;
                roleViewModel.ReturnMessage.Add(ex.Message);
                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, roleViewModel);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, roleViewModel);
            return response;
        }

        /// <summary>
        /// The DeleteRole
        /// </summary>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="roleViewModel">The roleViewModel<see cref="RoleViewModel"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage Delete(HttpRequestMessage request,
              [FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                var roleId = roleViewModel.RoleId;
                var role = _context.Roles.Find(roleId);

                _context.Roles.Remove(role);
                _context.SaveChanges();

                roleViewModel.ReturnStatus = true;
                roleViewModel.ReturnMessage.Add("Deleted Successfully!");
            }
            catch (Exception ex)
            {
                roleViewModel.ReturnStatus = false;
                roleViewModel.ReturnMessage.Add(ex.Message);
                var responseError = request.CreateResponse(HttpStatusCode.BadRequest, roleViewModel);
                return responseError;
            }

            var response = request.CreateResponse(HttpStatusCode.OK, roleViewModel);
            return response;
        }
    }
}
