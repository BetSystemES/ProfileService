using AutoMapper;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ProfileService.GRPC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic;
using System;

namespace ProfileService.GRPC.Services
{
    public class ProfileService : Profiler.ProfilerBase
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly IMapper _mapper;

        private readonly IProfileService _profileService;

        public ProfileService(ILogger<ProfileService> logger, IMapper mapper, IProfileService profileService)
        {
            _logger = logger;
            _mapper = mapper;
            _profileService = profileService;
        }

        public override async Task<BasicVoidResponce> AddPersonalData(PersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request);

            //profile service
            await _profileService.AddPersonalData(personalData, token);

            return new BasicVoidResponce();
        }


        public override async Task<PersonalDataResponce> GetPersonalDataById(ProfileByIdRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            
            //map
            Guid guid = _mapper.Map<Guid>(request.Id);

            //profile service
            var item = await _profileService.GetPersonalDataById(guid, token);

            //map back
            PersonalDataResponce personalDataResponce = _mapper.Map<PersonalDataResponce>(item);

            return personalDataResponce;
        }

        public override async Task<BasicVoidResponce> UpdatePersonalData(PersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request);

            //profile service
            await _profileService.UpdatePersonalData(personalData, token);

            return new BasicVoidResponce();
        }

        public override async Task<BasicVoidResponce> AddDiscount(Discount request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request);

            //profile service
            await _profileService.AddDiscount(bonus, token);

            return new BasicVoidResponce();
        }

        public override async Task<DiscountsResponce> GetDiscounts(ProfileByIdRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.Id);

            //profile service
            var items = await _profileService.GetDiscounts(guid, token);

            //map back
            DiscountsResponce discountsResponce = _mapper.Map<DiscountsResponce>(items);

            return discountsResponce;
        }

        public override async Task<BasicVoidResponce> UpdateDiscount(Discount request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request);

            //profile service
            await _profileService.UpdateDiscount(bonus, token);

            return new BasicVoidResponce();
        }

       
    }
}