using FizzWare.NBuilder;
using ProfileService.GRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.TestDataGeneratorsAndExtensions.Extensions
{
    public  static class DiscountObjectBuilderExtentions
    {
        public static ISingleObjectBuilder<Discount> DiscountBuilderId(this ISingleObjectBuilder<Discount> discount, string id, string personalId)
        {
            discount
                .With(x => x.Id = id)
                .With(x => x.Personalid = personalId);

            return discount;
        }

        public static ISingleObjectBuilder<Discount> DiscountBuilderValue(this ISingleObjectBuilder<Discount> discount, DiscountType discountType, double value)
        {
            discount
                .With(x => x.Type = discountType);

            return discountType switch
            {
                DiscountType.Discount =>
                    discount
                        .With(x => x.Discountvalue = value)
                        .With(x => x.Amount = 0),
                DiscountType.Amount =>
                    discount
                        .With(x => x.Amount = value)
                        .With(x => x.Discountvalue = 0),
                DiscountType.Unspecified => discount
                    .With(x => x.Discountvalue = 0)
                    .With(x => x.Amount = 0),
            };
        }

        public static ISingleObjectBuilder<Discount> DiscountBuilderAllValue(this ISingleObjectBuilder<Discount> discount, DiscountType discountType, double amountValue, double discountValue)
        {
            discount
                .With(x => x.Type = discountType)
                .With(x => x.Amount = amountValue)
                .With(x => x.Discountvalue = discountValue);

            return discount;
        }
    }
}