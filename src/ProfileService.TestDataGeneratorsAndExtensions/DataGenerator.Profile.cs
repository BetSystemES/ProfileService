using FizzWare.NBuilder;
using ProfileService.GRPC;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static UserProfile UserProfileGenerator(string profileId)
        {
            UserProfile userProfile = Builder<UserProfile>
                .CreateNew()
                .With(x => x.Id = profileId)
                .With(x => x.FirstName = "Not Empty")
                .With(x => x.Email = "test@email.com")
                .Build();

            return userProfile;
        }
    }
}