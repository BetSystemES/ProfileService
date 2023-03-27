using System.ComponentModel;

namespace ProfileService.BusinessLogic.Models.Enums
{
    public enum AuthRole
    {
        [Description("admin")]
        Admin,

        [Description("user")]
        User,
    }
}