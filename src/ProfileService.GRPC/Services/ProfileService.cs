using System.Security.Claims;
using AutoMapper;
using Grpc.Core;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models.Criterias;
using ProfileService.BusinessLogic.Models.Enums;
using ProfileService.BusinessLogic.Models.Enums.Extensions;

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

        public override async Task<AddProfileDataResponse> AddProfileData(AddProfileDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            ProfileData profileData = _mapper.Map<ProfileData>(request.UserProfile);

            //profile service
            await _profileService.AddProfileData(profileData, token);

            return new AddProfileDataResponse();
        }

        public override async Task<GetProfileDataByIdResponse> GetProfileDataById(GetProfileDataByIdRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.ProfileByIdRequest.Id);

            //profile service
            var item = await _profileService.GetProfileDataById(guid, token);

            //map back
            UserProfile userProfile = _mapper.Map<UserProfile>(item);

            return new GetProfileDataByIdResponse
            {
                UserProfile = userProfile
            };
        }

        public override async Task<UpdateProfileDataResponse> UpdateProfileData(UpdateProfileDataRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            ProfileData profileData = _mapper.Map<ProfileData>(request.UserProfile);

            //profile service
            await _profileService.UpdateProfileData(profileData, token);

            return new UpdateProfileDataResponse();
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
            Guid guid = _mapper.Map<Guid>(request.ProfileByIdRequest.Id);

            //profile service
            var items = await _profileService.GetDiscounts(guid, token);

            //map back
            IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>, IEnumerable<Discount>>(items);

            GetDiscountsResponse response = new GetDiscountsResponse();

            response.Discounts.AddRange(discounts);

            return response;
        }

        public override async Task<GetDiscountsResponse> GetDiscountsDepOnRole(GetDiscountsRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.ProfileByIdRequest.Id);

            //Get Role From Header and Check Role
            bool isReadyToUse = IsReadyToUse(context.GetHttpContext().User);

            //profile service
            var items = await _profileService.GetDiscountsDepOnRole(guid, isReadyToUse, token);

            //map back
            IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>, IEnumerable<Discount>>(items);

            GetDiscountsResponse response = new GetDiscountsResponse();

            response.Discounts.AddRange(discounts);

            return response;
        }

        //public override async Task<GetDiscountsResponse> GetDiscountsDepOnFilter(GetDiscountsWithFilterRequest request, ServerCallContext context)
        //{
        //    var token = context.CancellationToken;

        //    //map
        //    Guid guid = _mapper.Map<Guid>(request.ProfileByIdRequest.Id);
        //    PaginationCriteria paginationCriteria = _mapper.Map<PaginationCriteria>(request.DiscountFilter);

        //    //profile service
        //    var items = await _profileService.GetDiscountsWithFilter(guid, paginationCriteria, token);

        //    //map back
        //    IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>, IEnumerable<Discount>>(items);

        //    GetDiscountsResponse response = new GetDiscountsResponse();

        //    response.Discounts.AddRange(discounts);

        //    return response;
        //}

        public override async Task<GetPagedDiscountsResponse> GetPagedDiscounts(GetDiscountsWithFilterRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            //map
            Guid guid = _mapper.Map<Guid>(request.ProfileByIdRequest.Id);
            FilterCriteria filterCriteria = _mapper.Map<FilterCriteria>(request.DiscountFilter);

            //profile service
            var items = await _profileService.GetPagedDiscounts(filterCriteria, token);

            //map back
            IEnumerable<Discount> discounts = _mapper.Map<IEnumerable<Bonus>, IEnumerable<Discount>>(items.Data);

            GetPagedDiscountsResponse response = new GetPagedDiscountsResponse()
            {
                TotalCount = items.TotalCount
            };

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

        private bool IsReadyToUse(ClaimsPrincipal user)
        {
            var authRoles = user
                .FindFirst(ClaimTypes.Role)?.Value.Split(',')
                .Select(x => x.GetEnumItem<AuthRole>());

            if (authRoles == null) return true;

            return !authRoles.Contains(AuthRole.Admin);
        }
    }
}