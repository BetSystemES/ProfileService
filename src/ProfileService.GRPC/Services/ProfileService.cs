using AutoMapper;
using Grpc.Core;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;

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

        public override async Task<AddPersonalDataResponse> AddPersonalData(AddPersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request.Personalprofile);

            //profile service
            await _profileService.AddPersonalData(personalData, token);

            return new AddPersonalDataResponse();
        }
        
        public override async Task<GetPersonalDataByIdResponse> GetPersonalDataById(GetPersonalDataByIdRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            
            //map
            Guid guid = _mapper.Map<Guid>(request.Profilebyidrequest.Id);

            //profile service
            var item = await _profileService.GetPersonalDataById(guid, token);

            //map back
            PersonalProfile personalProfile = _mapper.Map<PersonalProfile>(item);

            return new GetPersonalDataByIdResponse
            {
                Personalprofile = personalProfile
            };
        }

        public override async Task<UpdatePersonalDataResponse> UpdatePersonalData(UpdatePersonalDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            PersonalData personalData = _mapper.Map<PersonalData>(request.Personalprofile);

            //profile service
            await _profileService.UpdatePersonalData(personalData, token);

            return new UpdatePersonalDataResponse();
        }

        public override async Task<AddDiscountResponse> AddDiscount(AddDiscountRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request.Discount);

            //profile service
            await _profileService.AddDiscount(bonus, token);

            return new AddDiscountResponse();
        }

        public override async Task<GetDiscountsResponse> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.Profilebyidrequest.Id);

            //profile service
            var items = await _profileService.GetDiscounts(guid, token);

            //map back
            IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>,IEnumerable<Discount>>(items);

            GetDiscountsResponse response = new GetDiscountsResponse();

            response.Discounts.AddRange(discounts);

            return response;
        }

        public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            //map
            Bonus bonus = _mapper.Map<Bonus>(request.Discount);

            //profile service
            await _profileService.UpdateDiscount(bonus, token);

            return new UpdateDiscountResponse();
        }
    }
}