using FizzWare.NBuilder;
using ProfileService.BusinessLogic.Entities;
using ProfileService.TestDataGeneratorsAndExtensions.Extensions;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static Bonus BonusGenerator(Guid bonusId, Guid profileId)
        {
            Bonus bonus = Builder<Bonus>
                .CreateNew()
                .With(x => x.BonusId = bonusId)
                .With(x => x.ProfileId = profileId)
                .Build();

            return bonus;
        }

        public static Bonus BonusGenerator(Guid bonusId, Guid profileId, ProfileData profileData)
        {
            Bonus bonus = BonusGenerator(bonusId, profileId).SetProfileData(profileData);
            return bonus;
        }
    }
}