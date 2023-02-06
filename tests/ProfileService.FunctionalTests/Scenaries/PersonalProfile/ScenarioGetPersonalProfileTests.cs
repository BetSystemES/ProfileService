using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;

using FluentAssertions;
using NScenario;
using ProfileService.BusinessLogic;
using static ProfileService.GRPC.Profiler;


namespace ProfileService.FunctionalTests.Scenaries
{
    public class ScenarioGetPersonalProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfilerClient _client;

        public ScenarioGetPersonalProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfilerClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioGetPersonaProfileById()
        {
            string id = Guid.NewGuid().ToString();

            PersonalProfile personalProfile = new()
            {
                Id = id,
                Name = "Pavel",
                Surname = "K",
                Phone = "444333222",
                Email = "PavelK@google.com"
            };

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

            var getPersonalDataByIdResponce = await scenario
                .Step($"Get PersonalDataById",
                async () =>
                {
                    var request = new GetPersonalDataByIdRequest()
                    {
                        Profilebyidrequest = new ProfileByIdRequest() { Id = id }
                    };
                    return await _client.GetPersonalDataByIdAsync(request);
                });

            var result = getPersonalDataByIdResponce.Personalprofile;

            result
                .Should()
                .NotBeNull()
                .Equals(personalProfile);
        }
    }
}
