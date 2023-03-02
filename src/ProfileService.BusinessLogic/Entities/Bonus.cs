using System.ComponentModel.DataAnnotations;
using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Entities
{
    public class Bonus
    {
        [Key] public Guid BonusId { get; set; }

        [Required] public Guid PersonalId { get; set; }
        public PersonalData PersonalData { get; set; }

        public bool isAlreadyUsed { get; set; } = false;
        public DiscountType DiscountType { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}