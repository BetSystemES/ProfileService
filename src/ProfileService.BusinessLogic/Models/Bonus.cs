using System.ComponentModel.DataAnnotations;

namespace ProfileService.BusinessLogic
{
    public class Bonus
    {
        [Key] Guid BonusId { get; set; }
        DiscountType DiscountType { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }

    enum DiscountType
    {
        Amount = 0,
        Discount = 1
    }
}