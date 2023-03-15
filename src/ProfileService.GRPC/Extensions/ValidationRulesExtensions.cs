using FluentValidation;

namespace ProfileService.GRPC.Extensions
{
    /// <summary>
    /// Validation rule for DiscountType
    /// </summary>
    public static class DiscountTypeValidationRule
    {
        /// <summary>
        /// Must the be valid DiscountType.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, DiscountType> MustBeValidDiscountType<T>(this IRuleBuilder<T, DiscountType> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .Must(e => e == DiscountType.Amount || e == DiscountType.Discount)
                .WithMessage("Received Unspecified DiscountType");

            return builderOptions;
        }
    }

    /// <summary>
    /// Validation rule for Discount
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
                case DiscountType.Amount when discount.Amount > 0 && discount.DiscountValue==0:
                case DiscountType.Discount when discount.DiscountValue > 0 && discount.Amount==0:
                    return true;
            }
        }
    }

    /// <summary>
    /// Validation rule for guid
    /// </summary>
    public static class GuidValidationRule
    {
        /// <summary>
        /// Must the be valid unique identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotNull()
                .NotEmpty()
                .Must(e => Guid.TryParse(e, out var guid))
                .WithMessage("Received invalid guid");

            return builderOptions;
        }

        /// <summary>
        /// Must the be valid dependent levels.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, IEnumerable<string>> MustBeValidDependentLevels<T>(this IRuleBuilder<T, IEnumerable<string>> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotEmpty()
                .Must(e => e.All(x => Guid.TryParse(x, out var guid)))
                .WithMessage("Received guid in dependent levels");

            return builderOptions;
        }
    }
}