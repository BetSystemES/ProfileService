using System.Runtime.InteropServices.ComTypes;
using FizzWare.NBuilder;
using ProfileService.BusinessLogic.Entities;
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

        public static Discount DiscountGenerator(string id, string personalId, DiscountType discountType, double value)
        {
            var discount = Builder<Discount>
                .CreateNew()
                .DiscountBuilderId(id, personalId)
                .DiscountBuilderValue(discountType, value)
                .Build();
            
            return discount;
        }
    }
}