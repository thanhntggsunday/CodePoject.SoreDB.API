namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RoleViewModel" />
    /// </summary>
    public class RoleViewModel : TransactionalInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewModel"/> class.
        /// </summary>
        public RoleViewModel() 
        {
            Roles = new List<RoleDTO>();
        }

        /// <summary>
        /// Gets or sets the RoleId
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Gets or sets the RoleName
        /// </summary>
        public string RoleName { get; set; }

       

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public List<RoleDTO> Roles { get; set; }
    }
}
