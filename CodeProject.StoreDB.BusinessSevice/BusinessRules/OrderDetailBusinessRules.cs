namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // OrderDetailBusinessRules
    /// <summary>
    /// Defines the <see cref="OrderDetailBusinessRules" />
    /// </summary>
    public class OrderDetailBusinessRules : AbstractValidator<OrderDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailBusinessRules"/> class.
        /// </summary>
        public OrderDetailBusinessRules()
        {
            RuleFor(p => p.OrderID).NotEmpty().WithMessage("OrderID is required.");
            RuleFor(p => p.ProductID).NotEmpty().WithMessage("ProductID is required.");
            RuleFor(p => p.Quantitty).NotEmpty().WithMessage("Quantitty is required.");
        }
    }
}
