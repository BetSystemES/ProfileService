using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.Discount;
using ProfileService.UnitTests.Validators.Discount.TestData;

namespace ProfileService.UnitTests.Validators.Discount
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
        public async Task AddProfileDataRequestValidator_Should_Be_Valid(GRPC.Discount discount)
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
        public async Task AddProfileDataRequestValidator_Should_Be_Invalid(GRPC.Discount discount)
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
