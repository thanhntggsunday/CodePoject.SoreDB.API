namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // PostCategoryBusinessRules
    /// <summary>
    /// Defines the <see cref="PostCategoryBusinessRules" />
    /// </summary>
    public class PostCategoryBusinessRules : AbstractValidator<PostCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostCategoryBusinessRules"/> class.
        /// </summary>
        public PostCategoryBusinessRules()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Post Name is required.");
            RuleFor(p => p.Alias).NotEmpty().WithMessage("Alias is required.");

            RuleFor(p => p.Status).NotEmpty().WithMessage("Status  is required.");
        }
    }
}
