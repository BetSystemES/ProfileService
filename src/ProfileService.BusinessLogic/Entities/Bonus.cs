using System.ComponentModel.DataAnnotations;
using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Entities
{
    public class Bonus
    {
        [Key] public Guid BonusId { get; set; }

        [Required] public Guid ProfileId { get; set; }
        public ProfileData ProfileData { get; set; }

        public bool IsAlreadyUsed { get; set; } = false;
        public bool IsEnabled { get; set; } = false;

        public DiscountType DiscountType { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}