namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // TagBusinessRules
    /// <summary>
    /// Defines the <see cref="TagBusinessRules" />
    /// </summary>
    public class TagBusinessRules : AbstractValidator<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagBusinessRules"/> class.
        /// </summary>
        public TagBusinessRules()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tag Name is required.");
            RuleFor(p => p.ID).NotEmpty().WithMessage("Tag ID is required.");
            RuleFor(p => p.Type).NotEmpty().WithMessage("Tag Type is required.");
        }
    }
}
