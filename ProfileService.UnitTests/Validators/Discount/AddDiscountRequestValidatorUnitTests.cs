using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentAssertions;
using ProfileService.GRPC;
using ProfileService.GRPC.Validators.PersonalData;
using System.Xml.Linq;
using ProfileService.GRPC.Validators.Discount;

namespace ProfileService.UnitTests.Validators
{
    public class AddDiscountRequestValidatorUnitTests
    {
        private readonly IValidator<AddDiscountRequest> _validator;

        public AddDiscountRequestValidatorUnitTests()
        {
            _validator = new AddDiscountRequestValidator();
        }

        [Theory]
        [MemberData(
            nameof(AddDiscountRequestData.AddDiscountRequestDataValid),
            MemberType = typeof(AddDiscountRequestData))]
        public async Task AddPersonalDataRequestValidator_Should_Be_Valid(Discount discount)
        {
            // Arrange
            var model = new AddDiscountRequest()
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
            nameof(AddDiscountRequestData.AddDiscountRequestDataInvalid),
            MemberType = typeof(AddDiscountRequestData))]
        public async Task AddPersonalDataRequestValidator_Should_Be_Invalid(Discount discount)
        {
            // Arrange
            var model = new AddDiscountRequest()
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
