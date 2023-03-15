using ProfileService.GRPC;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.UnitTests.Validators.Discount.TestData
{
    public static class UpdateDiscountRequestData
    {
        public static IEnumerable<object[]> UpdateDiscountRequestDataValid()
        {
            yield return new object[]
            {
                DiscountGenerator
                (
                    "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    DiscountType.Amount,
                    10
                )
            };

            yield return new object[]
            {
                DiscountGenerator
                (
                "34c92d2c-1f47-4a04-bffa-71101718b56d",
                "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                DiscountType.Discount,
                30
                )
            };
        }

        public static IEnumerable<object[]> UpdateDiscountRequestDataInvalid()
        {
            yield return new object[]
            {
                DiscountGenerator
                (
                    "34c92d2c",
                    "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                    DiscountType.Amount,
                    10
                )
            };

            yield return new object[]
            {
                DiscountGenerator
                (
                    "34c92d2c-1f47-4a04-bffa-71101718b56d",
                    "4864-8b5d-3c36a3c6f496",
                    DiscountType.Discount,
                    30
                )
            };

            yield return new object[]
            {
                DiscountGenerator
                (
                    DiscountType.Unspecified,
                    30
                )
            };

            yield return new object[]
            {
                DiscountGenerator
                (
                    DiscountType.Discount,
                    -200,
                    30
                )
            };

            yield return new object[]
            {
                DiscountGenerator
                (
                    DiscountType.Discount,
                    0,
                    -30
                )
            };
        }
    }
}