namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="UserRolesViewModels" />
    /// </summary>
    public class UserRolesViewModels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRolesViewModels"/> class.
        /// </summary>
        public UserRolesViewModels()
        {
            UserRolesList = new List<UserRolesDTO>();
        }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the RoleName
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ReturnStatus
        /// </summary>
        public bool ReturnStatus { get; set; }

        /// <summary>
        /// Gets or sets the ReturnMessage
        /// </summary>
        public string ReturnMessage { get; set; }

        /// <summary>
        /// Gets or sets the userRolesList
        /// </summary>
        public List<UserRolesDTO> UserRolesList { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
