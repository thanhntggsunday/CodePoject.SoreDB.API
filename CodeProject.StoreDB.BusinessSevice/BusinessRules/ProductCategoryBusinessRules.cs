namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // ProductCategoryBusinessRules
    /// <summary>
    /// Defines the <see cref="ProductCategoryBusinessRules" />
    /// </summary>
    public class ProductCategoryBusinessRules : AbstractValidator<ProductCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryBusinessRules"/> class.
        /// </summary>
        public ProductCategoryBusinessRules()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Alias).NotEmpty().WithMessage("Alis is required.");
            RuleFor(p => p.Status).NotEmpty().WithMessage("Status is required.");
        }
    }
}
