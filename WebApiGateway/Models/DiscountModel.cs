namespace WebApiGateway.Models
{
    public class DiscountModel
    {
        public string? Id { get; set; }
        public string? Personalid { get; set; }
        public bool Isalreadyused { get; set; }
        public DiscountModelType Type { get; set; }
        public double Amount { get; set; }
        public double Discountvalue { get; set; }
    }

    public enum DiscountModelType
    {
        Unspecified = 0,
        Amount = 1,
        Discount = 2,
    }
}
