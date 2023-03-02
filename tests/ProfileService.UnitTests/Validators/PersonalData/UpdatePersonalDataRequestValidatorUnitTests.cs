using FluentAssertions;
using FluentValidation;
using ProfileService.GRPC;
using ProfileService.GRPC.Infrastructure.Validators.PersonalData;

namespace ProfileService.UnitTests.Validators.PersonalData
{
    public class UpdatePersonalDataRequestValidatorUnitTests
    {
        private readonly IValidator<UpdatePersonalDataRequest> _validator;

        public UpdatePersonalDataRequestValidatorUnitTests()
        {
            _validator = new UpdatePersonalDataRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "Oleg", "Test@mail.ru")]
        public async Task AddPersonalDataRequestValidator_Should_Be_Valid(string id, string name, string email)
        {
            // Arrange
            var model = new UpdatePersonalDataRequest()
            {
                Personalprofile = new PersonalProfile()
                {
                        Id = id,   
                        Name = name,
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
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "", "Test@mail.ru")]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "Oleg", "")]
        public async Task AddPersonalDataRequestValidator_Should_Be_Invalid(string id, string name, string email)
        {
            // Arrange
            var model = new UpdatePersonalDataRequest()
            {
                Personalprofile = new PersonalProfile()
                {
                    Id = id,
                    Name = name,
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
