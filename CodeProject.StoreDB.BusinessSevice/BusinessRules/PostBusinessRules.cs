namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // PostBusinessRules
    /// <summary>
    /// Defines the <see cref="PostBusinessRules" />
    /// </summary>
    public class PostBusinessRules : AbstractValidator<Post>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostBusinessRules"/> class.
        /// </summary>
        public PostBusinessRules()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Post Name is required.");
            RuleFor(p => p.Alias).NotEmpty().WithMessage("Alias is required.");
            RuleFor(p => p.CategoryID).NotEmpty().WithMessage("Category  is required.");
            RuleFor(p => p.Status).NotEmpty().WithMessage("Status  is required.");
        }
    }
}
