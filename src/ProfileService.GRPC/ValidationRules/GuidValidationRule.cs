using FluentValidation;

namespace ProfileService.GRPC.ValidationRules
{
    // TODO: move functionality to ValidationRulesExtensions.cs file (description in DiscountTypeValidationRule.cs)
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
