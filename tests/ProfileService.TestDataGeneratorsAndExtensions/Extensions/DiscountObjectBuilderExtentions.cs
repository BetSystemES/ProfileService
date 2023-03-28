using FizzWare.NBuilder;
using ProfileService.GRPC;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public static class DiscountObjectBuilderExtentions
    {
        public static ISingleObjectBuilder<Discount> DiscountBuilderId(this ISingleObjectBuilder<Discount> discount, string id, string profileId)
        {
            discount
                .With(x => x.Id = id)
                .With(x => x.ProfileId = profileId)
                .With(x => x.IsEnabled = true); //true for user access

            return discount;
        }

        public static ISingleObjectBuilder<Discount> DiscountBuilderValue(this ISingleObjectBuilder<Discount> discount, DiscountType discountType, double value)
        {
            discount
                .With(x => x.Type = discountType);

            if (discountType == DiscountType.Unspecified)
                return discount
                     .With(x => x.Amount = 0);

            return discount
                    .With(x => x.Amount = value);
        }

        public static ISingleObjectBuilder<Discount> DiscountBuilderAllValue(this ISingleObjectBuilder<Discount> discount, DiscountType discountType, double amount)
        {
            discount
                .With(x => x.Type = discountType)
                .With(x => x.Amount = amount);

            return discount;
        }
    }
}