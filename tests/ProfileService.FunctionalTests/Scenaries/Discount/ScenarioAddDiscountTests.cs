using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;

using FluentAssertions;
using NScenario;
using static ProfileService.GRPC.ProfileService;
using DiscountType = ProfileService.GRPC.DiscountType;


namespace ProfileService.FunctionalTests.Scenaries
{
    public class ScenarioAddDiscountTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfileServiceClient _client;

        public ScenarioAddDiscountTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfileServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioAddDiscount()
        {

            string personalId= Guid.NewGuid().ToString();
            string discountId = Guid.NewGuid().ToString();

            PersonalProfile personalProfile = new()
            {
                Id = personalId,
                Name = "Pavel",
                Surname = "K",
                Phone = "444333222",
                Email = "PavelK@google.com"
            };

            Discount discount = new Discount()
            {
                Id = discountId,
                Personalid = personalId,
                Isalreadyused = false,
                Type =  DiscountType.Amount,
                Amount = 50,
                Discountvalue = 6,
            };
            

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"AddDiscount");

            var addPersonalDataResponse = await scenario
                .Step($"Add PersonalData",
                    async () =>
                    {
                        var request = new AddPersonalDataRequest()
                        {
                            Personalprofile = personalProfile
                        };

                        return await _client.AddPersonalDataAsync(request);
                    });

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

            var getDiscountsResponse = await scenario
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

            var result = getDiscountsResponse.Discounts.First();

            result
                .Should()
                .NotBeNull()
                .Equals(discount);
        }
    }
}
