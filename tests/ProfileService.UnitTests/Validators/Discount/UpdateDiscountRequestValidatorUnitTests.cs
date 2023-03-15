using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.Discount;
using ProfileService.UnitTests.Validators.Discount.TestData;

namespace ProfileService.UnitTests.Validators.Discount
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
        public async Task UpdateProfileDataRequestValidator_Should_Be_Valid(GRPC.Discount discount)
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
        public async Task UpdateProfileDataRequestValidator_Should_Be_Invalid(GRPC.Discount discount)
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
