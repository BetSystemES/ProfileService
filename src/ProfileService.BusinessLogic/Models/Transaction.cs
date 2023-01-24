using System.ComponentModel.DataAnnotations;



namespace ProfileService.BusinessLogic
{
    public class Transaction
    {
        [Key] public Guid TransactionId { get; set; }

        [Required] public decimal Amount { get; set; }

        [Required] public DateTime DateTime { get; set; }
    }
}