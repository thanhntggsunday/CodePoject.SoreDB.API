namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PostCategoryViewModel" />
    /// </summary>
    public class PostCategoryViewModel : TransactionalInformation
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ParentID
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HomeFlag
        /// </summary>
        public bool? HomeFlag { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the MetaKeyword
        /// </summary>
        public string MetaKeyword { get; set; }

        /// <summary>
        /// Gets or sets the MetaDescription
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the PostCategoryDTOs
        /// </summary>
        public List<PostCategoryDTO> PostCategoryDTOs { get; set; }
    }
}
