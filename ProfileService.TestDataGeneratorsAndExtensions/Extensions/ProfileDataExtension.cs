using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public static class ProfileDataExtension
    {
        public static ProfileData ChangeSurname(this ProfileData profileData, string surname)
        {
            profileData.LastName = surname;
            return profileData;
        }

        public static ProfileData ChangePhoneNumber(this ProfileData profileData, string phoneNumber)
        {
            profileData.PhoneNumber = phoneNumber;
            return profileData;
        }
    }
}
