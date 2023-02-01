using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.GRPC;

namespace ProfileService.UnitTests.Validators
{
    public static class DiscountExtension
    {
        public static Discount Init(this Discount model,
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
