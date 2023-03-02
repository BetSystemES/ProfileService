// TODO: remove unused/sort usings

using AutoMapper;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.GRPC.Infrastructure.Configuration
{
    // TODO: Change file location to ProfileService.Grpc.Infrastructure.Mappings
    // TODO: Rename cs file from AutoMapperConfiguration.cs to DataAccessProfile.cs
    // TODO: remove all empty lines
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            CreateMap<PersonalProfile, PersonalData>()
                .ForMember(dest => dest.PersonalId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.PhoneNumber,
                    opt =>
                    opt.MapFrom(src => src.Phone))
                .ForMember(x => x.Bonuses, opt => opt.Ignore());


            CreateMap<PersonalData, PersonalProfile>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.PersonalId.ToString()))
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
                        opt.MapFrom(src => src.Discountvalue))
                .ForMember(dest => dest.DiscountType,
                    opt =>
                        opt.MapFrom(src => src.Type))
                .ForMember(x => x.PersonalData, opt => opt.Ignore());


            CreateMap<Bonus, Discount>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.BonusId.ToString()))
                .ForMember(dest => dest.Discountvalue,
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
        }

    }
}