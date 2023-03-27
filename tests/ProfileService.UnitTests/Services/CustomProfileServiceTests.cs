using FluentAssertions;
using Moq;
using ProfileService.BusinessLogic.Entities;
using ProfileService.UnitTests.Infrastructure;
using ProfileService.UnitTests.Infrastructure.Builders;

namespace ProfileService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class CustomProfileServiceTests
    {
        [Fact]
        public async Task AddProfileDataTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceProfileDataRepositoryAdd()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.AddProfileData(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyProfileDataRepositoryAdd()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task GetProfileDataByIdTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetProfileServiceProfileData()
                .SetupProfileServiceProfileProviderGet()
                .Build();

            // Act
            var result = await verifier.CustomProfileService.GetProfileDataById(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().Be(verifier.ProfileData);

            verifier.VerifyProfileProviderGet();
        }

        [Fact]
        public async Task UpdateProfileDataTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceProfileDataRepositoryUpdate()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.UpdateProfileData(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyProfileDataRepositoryUpdate()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task DeleteProfileDataTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceProfileDataRepositoryRemove()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.DeleteProfileData(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyProfileDataRepositoryRemove()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task AddDiscountTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceBonusRepositoryAdd()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.AddDiscount(It.IsAny<Bonus>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyBonusRepositoryAdd()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task GetDiscountsTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetProfileServiceBonuses()
                .SetupProfileServiceBonusProviderFindBy()
                .Build();

            // Act
            var result = await verifier.CustomProfileService.GetDiscounts(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeEquivalentTo(verifier.Bonuses);

            verifier.VerifyBonusProviderFindBy();
        }

        [Fact]
        public async Task UpdateDiscountTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceBonusRepositoryUpdate()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.UpdateDiscount(It.IsAny<Bonus>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyBonusRepositoryUpdate()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task DeleteDiscountTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetupProfileServiceBonusRepositoryRemove()
                .SetupProfileServiceDataContextSaveChanges()
                .Build();

            // Act
            await verifier.CustomProfileService.DeleteDiscount(It.IsAny<Bonus>(), It.IsAny<CancellationToken>());

            // Assert
            verifier
                .VerifyBonusRepositoryRemove()
                .VerifyDataContextSaveChanges();
        }

        [Fact]
        public async Task GetPagedDiscountsTest()
        {
            // Arrange
            var verifier = new CustomProfileServiceVerifierBuilder()
                .SetProfileServiceFilterCriteria()
                .SetProfileServiceBonuses()
                .SetProfileServiceExpectedResponse()
                .SetupProfileServiceBonusProviderGetPaged()
                .SetupProfileServiceBonusProviderGetCount()
                .Build();

            // Act
            var result = await verifier.CustomProfileService
                .GetPagedDiscounts(verifier.FilterCriteria!, It.IsAny<CancellationToken>());

            // Assert
            result.Data.Should().BeEquivalentTo(verifier.ExpectedResponse!.Data);
            result.TotalCount.Should().Be(verifier.ExpectedResponse.TotalCount);

            verifier
                .VerifyBonusProviderGetPaged()
                .VerifyBonusProviderGetCount();
        }
    }
}
