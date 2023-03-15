using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.ProfileData
{
    /// <summary>
    /// Validation rules for <seealso cref="UpdateProfileDataRequest"/>
    /// </summary>
    public class UpdateProfileDataRequestValidator : AbstractValidator<UpdateProfileDataRequest>
    {
        public UpdateProfileDataRequestValidator()
        {
            RuleFor(e => e.UserProfile.Id)
                .MustBeValidGuid();

            RuleFor(e => e.UserProfile.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.UserProfile.Email)
                .EmailAddress();
        }
    }
}