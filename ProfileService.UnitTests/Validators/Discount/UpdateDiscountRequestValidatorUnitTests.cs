using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentAssertions;
using ProfileService.GRPC;

using System.Xml.Linq;
using ProfileService.GRPC.Validators.Discount;

namespace ProfileService.UnitTests.Validators
{
    public class UpdateDiscountRequestValidatorUnitTests
    {
        private readonly IValidator<UpdateDiscountRequest> _validator;

        public UpdateDiscountRequestValidatorUnitTests()
        {
            _validator = new UpdateDiscountRequestValidator();
        }

        [Theory]
        [MemberData(
            nameof(UpdateDiscountRequestData.UpdateDiscountRequestDataValid),
            MemberType = typeof(UpdateDiscountRequestData))]
        public async Task UpdatePersonalDataRequestValidator_Should_Be_Valid(Discount discount)
        {
            // Arrange
            var model = new UpdateDiscountRequest()
            {
                Discount = discount
            };
            
            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(true);
        }
        [Theory]
        [MemberData(
            nameof(UpdateDiscountRequestData.UpdateDiscountRequestDataInvalid),
            MemberType = typeof(UpdateDiscountRequestData))]
        public async Task UpdatePersonalDataRequestValidator_Should_Be_Invalid(Discount discount)
        {
            // Arrange
            var model = new UpdateDiscountRequest()
            {
                Discount = discount
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(false);
        }
    }
}
