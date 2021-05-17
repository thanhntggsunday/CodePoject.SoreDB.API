namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ProductViewModel" />
    /// </summary>
    public class ProductViewModel : TransactionalInformation
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
        /// Gets or sets the CategoryID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the MoreImages
        /// </summary>
        public string MoreImages { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the PromotionPrice
        /// </summary>
        public decimal? PromotionPrice { get; set; }

        /// <summary>
        /// Gets or sets the Warranty
        /// </summary>
        public int? Warranty { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HomeFlag
        /// </summary>
        public bool? HomeFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HotFlag
        /// </summary>
        public bool? HotFlag { get; set; }

        /// <summary>
        /// Gets or sets the ViewCount
        /// </summary>
        public int? ViewCount { get; set; }

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
        /// Gets or sets the Quantity
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the ProductSearchContent
        /// </summary>
        public ProductSearchContent ProductSearchContent { get; set; }

        /// <summary>
        /// Gets or sets the ProductDTOs
        /// </summary>
        public List<ProductDTO> ProductDTOs { get; set; } = new List<ProductDTO>();

        /// <summary>
        /// Gets or sets the LaptopDTOs
        /// </summary>
        public List<ProductDTO> LaptopDTOs { get; set; } = new List<ProductDTO>();

        /// <summary>
        /// Gets or sets the SmartPhonesDTOs
        /// </summary>
        public List<ProductDTO> SmartPhonesDTOs { get; set; } = new List<ProductDTO>();

       
    }
}
