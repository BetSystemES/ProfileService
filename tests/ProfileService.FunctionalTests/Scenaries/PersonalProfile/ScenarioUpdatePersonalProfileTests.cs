using FluentAssertions;
using NScenario;
using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;
using static ProfileService.GRPC.ProfileService;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.FunctionalTests.Scenaries.PersonalProfile
{
    public class ScenarioUpdatePersonalProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfileServiceClient _client;

        public ScenarioUpdatePersonalProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfileServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioUpdatePersonalProfile()
        {
            string id = Guid.NewGuid().ToString();

            var personalProfile = PersonalProfileGenerator(id);
            var personalProfile2 = PersonalProfileGenerator(id);
            personalProfile2.Phone = "5135135531";

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"UpdatePersonalProfile");

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

            var updatePersonalDataResponse = await scenario
                .Step($"Update PersonalProfile",
                async () =>
                {
                    var request = new UpdatePersonalDataRequest()
                    {
                        Personalprofile = personalProfile2
                    };

                    return await _client.UpdatePersonalDataAsync(request);
                });

            var getPersonalDataByIdResponse = await scenario
                .Step($"Get PersonalProfileById",
                async () =>
                {
                    var request = new GetPersonalDataByIdRequest()
                    {
                        Profilebyidrequest = new ProfileByIdRequest() { Id = id }
                    };
                    return await _client.GetPersonalDataByIdAsync(request);
                });

            var result = getPersonalDataByIdResponse.Personalprofile;

            result
                .Should()
                .NotBeNull()
                .Equals(personalProfile2);
        }
    }
}
