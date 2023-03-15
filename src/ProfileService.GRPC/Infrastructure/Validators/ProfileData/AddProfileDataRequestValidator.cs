using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.ProfileData
{
    /// <summary>
    /// Validation rules for <seealso cref="AddProfileDataRequest"/>
    /// </summary>
    public class AddProfileDataRequestValidator : AbstractValidator<AddProfileDataRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProfileDataRequestValidator"/> class.
        /// </summary>
        public AddProfileDataRequestValidator()
        {
            RuleFor(e => e.UserProfile.Id)
                .MustBeValidGuid();

            RuleFor(e => e.UserProfile.Email)
                .EmailAddress();
        }
    }
}