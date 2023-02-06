using ProfileService.FunctionalTests.Adapters;
using ProfileService.GRPC;
using Xunit.Abstractions;

using FluentAssertions;
using NScenario;
using ProfileService.BusinessLogic;
using static ProfileService.GRPC.Profiler;


namespace ProfileService.FunctionalTests.Scenaries
{
    public class ScenarioUpdatePersonalProfileTests : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProfilerClient _client;

        public ScenarioUpdatePersonalProfileTests(TestServerFixture factory,
            ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _client = new ProfilerClient(factory.GrpcChannel);
        }

        [Fact()]
        public async Task ScenarioUpdatePersonalProfile()
        {
            string id = Guid.NewGuid().ToString();

            PersonalProfile personalProfile = new()
            {
                Id = id,
                Name = "Pavel",
                Surname = "P",
                Phone = "444333222",
                Email = "PavelK@google.com"
            };

            PersonalProfile personalProfile2 = new()
            {
                Id = id,
                Name = "Pavel",
                Surname = "K",
                Phone = "444333222",
                Email = "PavelK@google.com"
            };


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

            var getPersonalDataByIdResponce = await scenario
                .Step($"Get PersonalProfileById",
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
                .Equals(personalProfile2);
        }
    }
}
