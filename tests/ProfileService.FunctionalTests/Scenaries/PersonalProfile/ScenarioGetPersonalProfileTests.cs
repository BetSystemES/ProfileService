using FluentAssertions;
using NScenario;
using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;
using static ProfileService.GRPC.ProfileService;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.FunctionalTests.Scenaries.PersonalProfile
{
    public class ScenarioGetPersonalProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfileServiceClient _client;

        public ScenarioGetPersonalProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfileServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioGetPersonaProfileById()
        {
            string id = Guid.NewGuid().ToString();
            var personalProfile = PersonalProfileGenerator(id);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"GetPersonalDataById");

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

            var getPersonalDataByIdResponse = await scenario
                .Step($"Get PersonalDataById",
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
                .Equals(personalProfile);
        }
    }
}
