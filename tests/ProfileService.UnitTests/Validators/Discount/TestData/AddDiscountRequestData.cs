using ProfileService.GRPC;
using ProfileService.UnitTests.Validators.Discount.TestHelpers;

namespace ProfileService.UnitTests.Validators.Discount.TestData
{
    public static class AddDiscountRequestData
    {
        public static IEnumerable<object[]> AddDiscountRequestDataValid()
        {
            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Amount,
                    amount: 10,
                    discountvalue: 0
                )
            };

            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Discount,
                    amount: 0,
                    discountvalue: 30
                )
            };

        }

        public static IEnumerable<object[]> AddDiscountRequestDataInvalid()
        {
            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Amount,
                    amount: 10,
                    discountvalue: 0
                )
            };

            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Discount,
                    amount: 0,
                    discountvalue: 30
                )
            };

            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Unspecified,
                    amount: 0,
                    discountvalue: 30
                )
            };

            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Discount,
                    amount: -200,
                    discountvalue: 30
                )
            };

            yield return new object[]
            {
                new GRPC.Discount().Init
                (
                    id: "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    personalId: "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    isalreadyused: false,
                    discountType: DiscountType.Discount,
                    amount: 0,
                    discountvalue: -30
                )
            };

        }
    }
}
