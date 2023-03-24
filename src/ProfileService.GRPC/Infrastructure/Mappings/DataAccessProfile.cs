using AutoMapper;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models.Criterias;

namespace ProfileService.GRPC.Infrastructure.Mappings
{
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            CreateMap<UserProfile, ProfileData>()
                .ForMember(dest => dest.ProfileId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.PhoneNumber,
                    opt =>
                    opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Bonuses, opt => opt.Ignore());

            CreateMap<ProfileData, UserProfile>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.ProfileId.ToString()))
                .ForMember(dest => dest.Phone,
                    opt =>
                        opt.MapFrom(src => src.PhoneNumber));

            CreateMap<IEnumerable<Discount>, IEnumerable<Bonus>>();

            CreateMap<Discount, Bonus>()
                .ForMember(dest => dest.BonusId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.Discount,
                    opt =>
                        opt.MapFrom(src => src.DiscountValue))
                .ForMember(dest => dest.DiscountType,
                    opt =>
                        opt.MapFrom(src => src.Type))
                .ForMember(x => x.ProfileData, opt => opt.Ignore());

            CreateMap<Bonus, Discount>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.BonusId.ToString()))
                .ForMember(dest => dest.DiscountValue,
                    opt =>
                        opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.DiscountType));

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));

            CreateMap<DiscountType, BusinessLogic.Models.Enums.DiscountType>().ReverseMap();

            CreateMap<FilterCriteria, DiscountFilter>()
                .ForMember(dest => dest.UserIds,
                opt =>
                    opt.MapFrom(src => src.UserIds.Select(x => x.ToString())));

            CreateMap<DiscountFilter, FilterCriteria>()
                .ForMember(dest => dest.UserIds,
                    opt =>
                        opt.MapFrom(src => src.UserIds.Select(Guid.Parse).ToList()));
        }
    }
}