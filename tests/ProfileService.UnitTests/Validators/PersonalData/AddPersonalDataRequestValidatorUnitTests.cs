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

namespace ProfileService.UnitTests.Validators
{
    public class AddPersonalDataRequestValidatorUnitTests
    {
        private readonly IValidator<AddPersonalDataRequest> _validator;

        public AddPersonalDataRequestValidatorUnitTests()
        {
            _validator = new AddPersonalDataRequestValidator();
        }

        [Theory]
        [InlineData("c0631390-2ac4-4946-b172-501e173f47d6", "Oleg", "Test@mail.ru")]
        public async Task AddPersonalDataRequestValidator_Should_Be_Valid(string id, string name, string email)
        {
            // Arrange
            var model = new AddPersonalDataRequest()
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
            var model = new AddPersonalDataRequest()
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
