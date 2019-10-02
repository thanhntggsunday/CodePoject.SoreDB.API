namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    /// <summary>
    /// Defines the <see cref="ProductBusinessRules" />
    /// </summary>
    public class ProductBusinessRules : AbstractValidator<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBusinessRules"/> class.
        /// </summary>
        public ProductBusinessRules()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.Alias).NotEmpty().WithMessage("Alias is required.");
            RuleFor(p => p.CategoryID).NotEmpty().WithMessage("Category is required.");
            RuleFor(p => p.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Price is required.");
        }
    }
}
