using FluentAssertions;
using NScenario;
using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;
using static ProfileService.GRPC.ProfileService;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.FunctionalTests.Scenaries.Profile
{
    public class ScenarioGetProfileProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfileServiceClient _client;

        public ScenarioGetProfileProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfileServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioGetPersonaProfileById()
        {
            string id = Guid.NewGuid().ToString();
            var personalProfile = UserProfileGenerator(id);

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"GetPersonalDataById");

            var addPersonalDataResponse = await scenario
                .Step($"Add PersonalData",
                async () =>
                {
                    var request = new AddProfileDataRequest()
                    {
                        UserProfile = personalProfile
                    };

                    return await _client.AddProfileDataAsync(request);
                });

            var getPersonalDataByIdResponse = await scenario
                .Step($"Get PersonalDataById",
                async () =>
                {
                    var request = new GetProfileDataByIdRequest()
                    {
                        ProfileByIdRequest = new ProfileByIdRequest() { Id = id }
                    };
                    return await _client.GetProfileDataByIdAsync(request);
                });

            var result = getPersonalDataByIdResponse.UserProfile;

            result
                .Should()
                .NotBeNull()
                .Equals(personalProfile);
        }
    }
}
