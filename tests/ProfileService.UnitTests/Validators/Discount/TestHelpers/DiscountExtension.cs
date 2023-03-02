using ProfileService.GRPC;

namespace ProfileService.UnitTests.Validators.Discount.TestHelpers
{
    public static class DiscountExtension
    {
        public static GRPC.Discount Init(this GRPC.Discount model,
            string id,
            string personalId,
            bool isalreadyused,
            DiscountType discountType,
            double amount,
            double discountvalue)
        {
            model.Id = id;
            model.Personalid = personalId;
            model.Isalreadyused = isalreadyused;
            model.Type = discountType;
            model.Amount = amount;
            model.Discountvalue = discountvalue;

            return model;
        }
    }
}
