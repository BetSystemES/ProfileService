using FizzWare.NBuilder;
using ProfileService.BusinessLogic.Entities;
using ProfileService.TestDataGeneratorsAndExtensions.Extensions;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static Bonus BonusGenerator(Guid bonusId, Guid personalId)
        {
            Bonus bonus = Builder<Bonus>
                .CreateNew()
                .With(x => x.BonusId = bonusId)
                .With(x => x.PersonalId = personalId)
                .Build();

            return bonus;
        }

        public static Bonus BonusGenerator(Guid bonusId, Guid personalId, PersonalData personalData)
        {
            Bonus bonus = BonusGenerator(bonusId, personalId).SetPersonalData(personalData);
            return bonus;
        }
    }
}