using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public static class PersonalDataExtension
    {
        public static PersonalData ChangeSurname(this PersonalData personalData, string surname)
        {
            personalData.Surname = surname;
            return personalData;
        }

        public static PersonalData ChangePhoneNumber(this PersonalData personalData, string phoneNumber)
        {
            personalData.PhoneNumber = phoneNumber;
            return personalData;
        }
    }
}
