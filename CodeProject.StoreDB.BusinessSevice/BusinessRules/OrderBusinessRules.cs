namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // OrderBusinessRules
    /// <summary>
    /// Defines the <see cref="OrderBusinessRules" />
    /// </summary>
    public class OrderBusinessRules : AbstractValidator<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBusinessRules"/> class.
        /// </summary>
        public OrderBusinessRules()
        {
            RuleFor(p => p.CustomerName).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.CustomerEmail).NotEmpty().WithMessage("Email is required.");
            RuleFor(p => p.CustomerAddress).NotEmpty().WithMessage("Address is required.");
            RuleFor(p => p.CustomerMessage).NotEmpty().WithMessage("Message is required.");
            RuleFor(p => p.CustomerMobile).NotEmpty().WithMessage("Mobile is required.");
        }
    }
}
