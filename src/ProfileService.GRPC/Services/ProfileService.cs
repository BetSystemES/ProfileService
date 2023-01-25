using AutoMapper;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ProfileService.GRPC;
using ProfileService.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileService.GRPC.Services
{
    public class ProfileService : Profiler.ProfilerBase
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly IMapper _mapper;

        public ProfileService(ILogger<ProfileService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public override Task<PersonalDataResponce> GetPersonalDataById(ProfileByIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PersonalDataResponce());
        }

        public override Task<BasicVoidResponce> UpdatePersonalDataById(PersonalDataRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BasicVoidResponce());
        }

        public override Task<DiscountsResponce> GetDiscounts(ProfileByIdRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DiscountsResponce());
        }

        public override Task<BasicVoidResponce> UpdateDiscount(Discount request, ServerCallContext context)
        {
            return Task.FromResult(new BasicVoidResponce());
        }

       
    }
}