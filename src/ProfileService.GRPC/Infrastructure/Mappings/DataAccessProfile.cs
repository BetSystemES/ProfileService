using AutoMapper;
using Google.Protobuf.WellKnownTypes;
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
                .ForMember(dest => dest.CreateDate,
                    opt =>
                        opt.MapFrom(src => src.CreateDate.ToDateTimeOffset()))
                .ForMember(dest => dest.DiscountType,
                    opt =>
                        opt.MapFrom(src => src.Type))
                .ForMember(x => x.ProfileData, opt => opt.Ignore());

            CreateMap<Bonus, Discount>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.BonusId.ToString()))
                .ForMember(dest => dest.CreateDate,
                    opt =>
                        opt.MapFrom(src => src.CreateDate.ToTimestamp()))
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
                    opt.MapFrom(src => src.UserIds.Select(x => x.ToString())))
                .ForMember(dest => dest.StartDate,
                    opt =>
                        opt.MapFrom(src => src.StartDate != null ? ((DateTimeOffset)src.StartDate!).ToTimestamp() : (DateTimeOffset.MinValue).ToTimestamp()))
                .ForMember(dest => dest.EndDate,
                    opt =>
                        opt.MapFrom(src => src.EndDate != null ? ((DateTimeOffset)src.EndDate!).ToTimestamp() : (DateTimeOffset.MinValue).ToTimestamp()));

            CreateMap<DiscountFilter, FilterCriteria>()
                .ForMember(dest => dest.UserIds,
                    opt =>
                        opt.MapFrom(src => src.UserIds.Select(Guid.Parse).ToList()))
                .ForMember(dest => dest.PageSize,
                    opt =>
                        opt.MapFrom(src => src.PageSize == -1 ? (int?)null : src.PageSize))
                .ForMember(dest => dest.PageNumber,
                    opt =>
                        opt.MapFrom(src => src.PageNumber == -1 ? (int?)null : src.PageNumber))
                .ForMember(dest => dest.StartDate,
                    opt =>
                        opt.MapFrom(src => src.StartDate == (DateTimeOffset.MinValue).ToTimestamp() ? (DateTimeOffset?) null : src.StartDate.ToDateTimeOffset()))
                .ForMember(dest => dest.EndDate,
                    opt =>
                        opt.MapFrom(src => src.EndDate == (DateTimeOffset.MinValue).ToTimestamp() ? (DateTimeOffset?) null : src.EndDate.ToDateTimeOffset()));
        }
    }
}