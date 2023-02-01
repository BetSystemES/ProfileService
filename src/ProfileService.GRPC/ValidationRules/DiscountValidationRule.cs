using FluentValidation;

namespace ProfileService.GRPC.ValidationRules
{
    /// <summary>
    /// Validation rule for guid
    /// </summary>
    public static class DiscountValidationRule
    {
        /// <summary>
        /// Must the be valid Discount.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, Discount> MustBeValidDiscount<T>(this IRuleBuilder<T, Discount> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .Must(e => CheckDiscount(e))
                .WithMessage("Received Invalid Discount");

            return builderOptions;
        }

        private static bool CheckDiscount(Discount discount)
        {
            if (discount == null)
            {
                return false;
            }
            switch (discount.Type)
            {
                case DiscountType.Unspecified:
                default:
                    return false;
                case DiscountType.Amount when discount.Amount > 0 && discount.Discountvalue==0:
                case DiscountType.Discount when discount.Discountvalue > 0 && discount.Amount==0:
                    return true;
            }
        }
    }
}
