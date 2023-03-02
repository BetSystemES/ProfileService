using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.PersonalData
{
    // TODO: Change file location to ProfileService.Grpc.Infrastructure.Validators.PersonalData
    /// <summary>
    /// Validation rules for <seealso cref="UpdatePersonalDataRequest"/>
    /// </summary>
    public class UpdatePersonalDataRequestValidator : AbstractValidator<UpdatePersonalDataRequest>
    {
        /// <summary>UpdatePersonalDataRequestValidator"/> class.
        /// </summary>
        public UpdatePersonalDataRequestValidator()
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
