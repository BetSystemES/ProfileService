using FluentValidation;
using ProfileService.GRPC.ValidationRules;

namespace ProfileService.GRPC.Validators.PersonalData
{
    /// <summary>
    /// Validation rules for <seealso cref="AddPersonalDataRequest"/>
    /// </summary>
    public class AddPersonalDataRequestValidator : AbstractValidator<AddPersonalDataRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPersonalDataRequestValidator"/> class.
        /// </summary>
        public AddPersonalDataRequestValidator()
        {
            RuleFor(e => e.Personalprofile.Id)
                .MustBeValidGuid();

            RuleFor(e => e.Personalprofile.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.Personalprofile.Email)
                .EmailAddress();
        }
    }
}
