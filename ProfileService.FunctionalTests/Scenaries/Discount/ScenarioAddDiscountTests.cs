using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;

using FluentAssertions;
using NScenario;
using ProfileService.BusinessLogic;
using static ProfileService.GRPC.Profiler;
using DiscountType = ProfileService.GRPC.DiscountType;


namespace ProfileService.FunctionalTests.Scenaries
{
    public class ScenarioAddDiscountTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfilerClient _client;

        public ScenarioAddDiscountTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfilerClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioUpdateDiscount()
        {
           
            Discount discount = new Discount()
            {
                Id = "34c92d2c-1f47-4a04-bffa-71101718b56d",
                Personalid = "8f902da9-e152-4864-8b5d-3c36a3c6f496",
                Isalreadyused = false,
                Type =  DiscountType.Amount,
                Amount = 50,
                Discountvalue = 6,
            };
        

            string personalId = discount.Personalid;

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"AddDiscount");

            var addDiscountResponse = await scenario
                .Step($"Add Discount",
                async () =>
                {
                    var request = new AddDiscountRequest()
                    {
                        Discount = discount
                    };

                    return await _client.AddDiscountAsync(request);
                });

            var getDiscountsResponce = await scenario
                .Step($"Get Discounts",
                async () =>
                {
                    var request = new GetDiscountsRequest()
                    {
                        Profilebyidrequest = new ProfileByIdRequest()
                        {
                            Id = personalId,
                        }
                    };
                    return await _client.GetDiscountsAsync(request);
                });

            var result = getDiscountsResponce.Discounts.First();

            result
                .Should()
                .NotBeNull()
                .Equals(discount);
        }
    }
}
