using FluentValidation;
using ProfileService.GRPC.Extensions;

namespace ProfileService.GRPC.Infrastructure.Validators.PersonalData
{
    /// <summary>
    /// Validation rules for <seealso cref="GetPersonalDataByIdRequest"/>
    /// </summary>
    public class GetPersonalDataByIdRequestValidator : AbstractValidator<GetPersonalDataByIdRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonalDataByIdRequestValidator"/> class.
        /// </summary>
        public GetPersonalDataByIdRequestValidator()
        {
            RuleFor(e => e.Profilebyidrequest.Id)
                .MustBeValidGuid();
        }
    }
}