using FluentValidation;

namespace ProfileService.GRPC.ValidationRules
{
    // TODO: remove all empty lines
    // TODO: change file location to ProfileService.GRPC.Extensions
    // TODO: rename file to ValidationRulesExtensions
    /// <summary>
    /// Validation rule for guid
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
}
