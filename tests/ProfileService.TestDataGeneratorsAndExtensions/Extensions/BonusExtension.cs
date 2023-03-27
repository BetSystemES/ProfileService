using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public static class BonusExtension
    {
        public static Bonus ChangeisAlreadyUsed(this Bonus bonus, bool isAlreadyUsed)
        {
            bonus.IsAlreadyUsed = isAlreadyUsed;
            return bonus;
        }

        public static Bonus ChangeAmount(this Bonus bonus, decimal amount)
        {
            bonus.Amount = amount;
            return bonus;
        }

        public static Bonus SetProfileData(this Bonus bonus, ProfileData profileData)
        {
            bonus.ProfileData = profileData;
            return bonus;
        }
    }
}