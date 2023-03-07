using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public static class BonusExtension
    {
        public static Bonus ChangeisAlreadyUsed(this Bonus bonus, bool isAlreadyUsed)
        {
            bonus.isAlreadyUsed = isAlreadyUsed;
            return bonus;
        }

        public static Bonus ChangeAmount(this Bonus bonus, decimal amount)
        {
            bonus.Amount = amount;
            return bonus;
        }

        public static Bonus SetPersonalData(this Bonus bonus, PersonalData personalData)
        {
            bonus.PersonalData = personalData;
            return bonus;
        }
    }
}
