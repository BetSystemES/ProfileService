using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace ProfileService.BusinessLogic
{
    public class Bonus
    {
        [Key] public Guid BonusId { get; set; }

        public Guid PersonalId { get; set; } //???

        public bool isAlreadyUsed { get; set; } = false;
        public DiscountType DiscountType { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }

    public enum DiscountType
    {
        Amount = 0,
        Discount = 1
    }
}