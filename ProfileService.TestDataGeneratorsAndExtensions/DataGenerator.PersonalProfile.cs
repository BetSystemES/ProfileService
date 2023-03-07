using FizzWare.NBuilder;
using ProfileService.GRPC;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static PersonalProfile PersonalProfileGenerator(string personalId)
        {
            PersonalProfile personalProfile = Builder<PersonalProfile>
                .CreateNew()
                .With(x => x.Id = personalId)
                .With(x => x.Name = "Not Empty")
                .With(x => x.Email = "test@email.com")
                .Build();

            return personalProfile;
        }
    }
}