namespace CodeProject.StoreDB.BusinessSevice.BusinessRules
{
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation;

    // CartItemBusinessRules
    /// <summary>
    /// Defines the <see cref="CartItemBusinessRules" />
    /// </summary>
    public class CartItemBusinessRules : AbstractValidator<CartItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemBusinessRules"/> class.
        /// </summary>
        public CartItemBusinessRules()
        {
        }
    }
}
