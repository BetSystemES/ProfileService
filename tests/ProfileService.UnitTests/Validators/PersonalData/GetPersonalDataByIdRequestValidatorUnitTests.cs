using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.PersonalData;

namespace ProfileService.UnitTests.Validators.PersonalData
{
    public class GetPersonalDataByIdRequestValidatorUnitTests
    {
        private readonly IValidator<GetPersonalDataByIdRequest> _validator;

        public GetPersonalDataByIdRequestValidatorUnitTests()
        {
            _validator = new GetPersonalDataByIdRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6")]
        public async Task GetPersonalDataByIdRequestValidator_Should_Be_Valid(string id)
        {
            // Arrange
            var model = new GetPersonalDataByIdRequest()
            {
                Profilebyidrequest = new ProfileByIdRequest()
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
        public async Task GetPersonalDataByIdRequestValidator_Should_Be_Invalid(string id)
        {
            // Arrange
            var model = new GetPersonalDataByIdRequest()
            {
                Profilebyidrequest = new ProfileByIdRequest()
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
