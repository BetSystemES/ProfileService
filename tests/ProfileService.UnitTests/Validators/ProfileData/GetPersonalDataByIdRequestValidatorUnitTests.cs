using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.ProfileData;

namespace ProfileService.UnitTests.Validators.ProfileData
{
    public class GetProfileDataByIdRequestValidatorUnitTests
    {
        private readonly IValidator<GetProfileDataByIdRequest> _validator;

        public GetProfileDataByIdRequestValidatorUnitTests()
        {
            _validator = new GetProfileDataByIdRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6")]
        public async Task GetProfileDataByIdRequestValidator_Should_Be_Valid(string id)
        {
            // Arrange
            var model = new GetProfileDataByIdRequest()
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
            var model = new GetProfileDataByIdRequest()
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