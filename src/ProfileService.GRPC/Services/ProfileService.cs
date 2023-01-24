using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ProfileService.GRPC;

namespace ProfileService.GRPC.Services
{
    public class ProfileService : Profiler.ProfilerBase
    {
        private readonly ILogger<ProfileService> _logger;
        public ProfileService(ILogger<ProfileService> logger)
        {
            _logger = logger;
        }

        public override Task<PersonalDataResponce> GetPersonalData(PersonalRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PersonalDataResponce());
        }

        public override Task<TransactionsResponce> GetTransactions(PersonalRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TransactionsResponce());
        }

        public override Task<DiscountsResponce> GetDiscounts(PersonalRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DiscountsResponce());
        }
    }
}