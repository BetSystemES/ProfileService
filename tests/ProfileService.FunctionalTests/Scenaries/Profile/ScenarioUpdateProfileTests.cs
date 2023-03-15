using FluentAssertions;
using NScenario;
using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;
using static ProfileService.GRPC.ProfileService;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.FunctionalTests.Scenaries.Profile
{
    public class ScenarioUpdateProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfileServiceClient _client;

        public ScenarioUpdateProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfileServiceClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioUpdatePersonalProfile()
        {
            string id = Guid.NewGuid().ToString();

            var personalProfile = UserProfileGenerator(id);
            var personalProfile2 = UserProfileGenerator(id);
            personalProfile2.Phone = "5135135531";

            var scenario = TestScenarioFactory.Default(
                new XUnitOutputAdapter(_outputHelper),
                testMethodName: $"UpdatePersonalProfile");

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

            var updatePersonalDataResponse = await scenario
                .Step($"Update PersonalProfile",
                async () =>
                {
                    var request = new UpdateProfileDataRequest()
                    {
                        UserProfile = personalProfile2
                    };

                    return await _client.UpdateProfileDataAsync(request);
                });

            var getPersonalDataByIdResponse = await scenario
                .Step($"Get PersonalProfileById",
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
                .Equals(personalProfile2);
        }
    }
}
