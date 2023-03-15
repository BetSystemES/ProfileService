using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.ProfileData
{
    /// <summary>
    /// Validation rules for <seealso cref="GetProfileDataByIdRequest"/>
    /// </summary>
    public class GetProfileDataByIdRequestValidator : AbstractValidator<GetProfileDataByIdRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileDataByIdRequestValidator"/> class.
        /// </summary>
        public GetProfileDataByIdRequestValidator()
        {
            RuleFor(e => e.ProfileByIdRequest.Id)
                .MustBeValidGuid();
        }
    }
}