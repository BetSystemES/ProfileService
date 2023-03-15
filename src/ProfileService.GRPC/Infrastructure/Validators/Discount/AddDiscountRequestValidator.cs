using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.Discount
{
    /// <summary>
    /// Validation rules for <seealso cref="AddDiscountRequest"/>
    /// </summary>
    public class AddDiscountRequestValidator : AbstractValidator<AddDiscountRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddDiscountRequestValidator"/> class.
        /// </summary>
        public AddDiscountRequestValidator()
        {
            RuleFor(e => e.Discount.Id)
                .MustBeValidGuid();

            RuleFor(e => e.Discount.ProfileId)
                .MustBeValidGuid();

            RuleFor(e => e.Discount.Amount).Must(e => e >= 0);

            RuleFor(e => e.Discount.DiscountValue).Must(e => e >= 0);

            RuleFor(e => e.Discount.Type)
                .MustBeValidDiscountType();
        }
    }
}