using FizzWare.NBuilder;
using ProfileService.GRPC;
using ProfileService.TestDataGeneratorsAndExtensions.Extensions;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static Discount DiscountGenerator(DiscountType discountType, double value)
        {
            var discount = Builder<Discount>
                .CreateNew()
                .DiscountBuilderId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                .DiscountBuilderValue(discountType, value)
                .Build();

            return discount;
        }

        public static Discount DiscountGenerator(DiscountType discountType, double amountValue, double discountValue)
        {
            var discount = Builder<Discount>
                .CreateNew()
                .DiscountBuilderId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                .DiscountBuilderAllValue(discountType, amountValue, discountValue)
                .Build();

            return discount;
        }

        public static Discount DiscountGenerator(string id, string profileId, DiscountType discountType, double value)
        {
            var discount = Builder<Discount>
                .CreateNew()
                .DiscountBuilderId(id, profileId)
                .DiscountBuilderValue(discountType, value)
                .Build();

            return discount;
        }
    }
}