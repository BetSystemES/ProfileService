using System.ComponentModel;

namespace ProfileService.BusinessLogic.Models.Enums.Extensions
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description) ? attributes[0].Description : enumValue.ToString();
        }

        public static T GetEnumItem<T>(this string descriptionToMatch) where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .FirstOrDefault(v => string.Equals(v.GetDescription(), descriptionToMatch));
        }
    }
}