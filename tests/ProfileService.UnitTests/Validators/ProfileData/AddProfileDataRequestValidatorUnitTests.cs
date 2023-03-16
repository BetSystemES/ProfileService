using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.ProfileData;

namespace ProfileService.UnitTests.Validators.ProfileData
{
    public class AddProfileDataRequestValidatorUnitTests
    {
        private readonly IValidator<AddProfileDataRequest> _validator;

        public AddProfileDataRequestValidatorUnitTests()
        {
            _validator = new AddProfileDataRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "Oleg", "Test@mail.ru")]
        public async Task AddProfileDataRequestValidator_Should_Be_Valid(string id, string name, string email)
        {
            // Arrange
            var model = new AddProfileDataRequest()
            {
                UserProfile = new UserProfile()
                {
                    Id = id,
                    FirstName = name,
                    Email = email
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
        [InlineData("c0631390", "Oleg", "Test@mail.ru")]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "Oleg", "")]
        public async Task AddProfileDataRequestValidator_Should_Be_Invalid(string id, string name, string email)
        {
            // Arrange
            var model = new AddProfileDataRequest()
            {
                UserProfile = new UserProfile()
                {
                    Id = id,
                    FirstName = name,
                    Email = email
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