﻿using FluentValidation;
using ProfileService.GRPC.ValidationRules;

namespace ProfileService.GRPC.Validators.PersonalData
{
    // TODO: Change file location to ProfileService.Grpc.Infrastructure.Validators.PersonalData
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
