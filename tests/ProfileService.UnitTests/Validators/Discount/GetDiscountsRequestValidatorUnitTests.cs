using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.Discount;

namespace ProfileService.UnitTests.Validators.Discount
{
    public class GetDiscountsRequestValidatorUnitTests
    {
        private readonly IValidator<GetDiscountsRequest> _validator;

        public GetDiscountsRequestValidatorUnitTests()
        {
            _validator = new GetDiscountsRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6")]
        public async Task GetProfileDataByIdRequestValidator_Should_Be_Valid(string id)
        {
            // Arrange
            var model = new GetDiscountsRequest()
            {
                ProfileByIdRequest = new ProfileByIdRequest()
                {
                    Id = id
                }
            };

            // Act
            var result = await _validator.ValidateAsync(model);

            // Assert
            result.IsValid
                .Should()
                .Be(true);
        }

        [Theory]
        [InlineData("c0631390")]
        [InlineData("")]
        public async Task GetProfileDataByIdRequestValidator_Should_Be_Invalid(string id)
        {
            // Arrange
            var model = new GetDiscountsRequest()
            {
                ProfileByIdRequest = new ProfileByIdRequest()
                {
                    Id = id
                }
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
