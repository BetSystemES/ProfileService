using AutoMapper;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using ProfileService.GRPC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic;
using System;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using ProfileService.EntityModels.Models;

namespace ProfileService.GRPC.Services
{
    public class ProfileService : GRPC.ProfileService.ProfileServiceBase
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

        public override async Task<AddPersonalDataResponce> AddPersonalData(AddPersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request.Personalprofile);

            //profile service
            await _profileService.AddPersonalData(personalData, token);

            return new AddPersonalDataResponce();
        }
        
        public override async Task<GetPersonalDataByIdResponce> GetPersonalDataById(GetPersonalDataByIdRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            
            //map
            Guid guid = _mapper.Map<Guid>(request.Profilebyidrequest.Id);

            //profile service
            var item = await _profileService.GetPersonalDataById(guid, token);

            //map back

            PersonalProfile personalProfile = _mapper.Map<PersonalProfile>(item);

            return new GetPersonalDataByIdResponce
            {
                Personalprofile = personalProfile
            };
        }

        public override async Task<UpdatePersonalDataResponce> UpdatePersonalData(UpdatePersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request.Personalprofile);

            //profile service
            await _profileService.UpdatePersonalData(personalData, token);

            return new UpdatePersonalDataResponce();
        }

        public override async Task<AddDiscountResponce> AddDiscount(AddDiscountRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request.Discount);

            //profile service
            await _profileService.AddDiscount(bonus, token);

            return new AddDiscountResponce();
        }

        public override async Task<GetDiscountsResponce> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.Profilebyidrequest.Id);

            //profile service
            var items = await _profileService.GetDiscounts(guid, token);

            //map back
            IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>,IEnumerable<Discount>>(items);

            GetDiscountsResponce responce = new GetDiscountsResponce();

            responce.Discounts.AddRange(discounts);

            return responce;
        }

        public override async Task<UpdateDiscountResponce> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request.Discount);

            //profile service
            await _profileService.UpdateDiscount(bonus, token);

            return new UpdateDiscountResponce();
        }

       
    }
}