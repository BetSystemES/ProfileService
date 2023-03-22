using FizzWare.NBuilder;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static ProfileData ProfileDataGenerator(Guid profileId)
        {
            ProfileData profileData = Builder<ProfileData>
                .CreateNew()
                .With(x => x.ProfileId = profileId)
                .Build();

            return profileData;
        }
    }
}