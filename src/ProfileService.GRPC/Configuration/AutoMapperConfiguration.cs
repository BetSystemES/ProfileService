using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.BusinessLogic;
using ProfileService.GRPC;

namespace ProfileService.GRPC.Configuration
{
    public class DataAccessProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile()
        {
            CreateMap<PersonalData, PersonalProfile>().ReverseMap();
            CreateMap<PersonalProfile, PersonalData>().ReverseMap();

            CreateMap<IEnumerable<Bonus>, GetDiscountsResponce>().ReverseMap();
            CreateMap<GetDiscountsResponce, IEnumerable<Bonus>>().ReverseMap();

            CreateMap<Bonus, Discount>().ReverseMap();
            CreateMap<Discount, Bonus>().ReverseMap();

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));
        }
        
    }

    public class DataAccessProfile2 : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile2()
        {
            CreateMap<PersonalData, PersonalProfile>()
                .ReverseMap()
                .ForMember(dest=>dest.PersonalId,
                    opt =>
                        opt.MapFrom(c => Guid.Parse(c.Id)))
                .ForMember(dest=>dest.PhoneNumber,
                    opt =>
                        opt.MapFrom(c => c.Phone.ToString()));


            CreateMap<PersonalProfile, PersonalData>()
                .ReverseMap()
                .ForMember(dest=>dest.Id,
                    opt =>
                        opt.MapFrom(c => c.PersonalId.ToString()))
                .ForMember(dest=>dest.Phone,
                    opt =>
                        opt.MapFrom(c => c.PhoneNumber));

            CreateMap<IEnumerable<Bonus>, GetDiscountsResponce>()
                .ReverseMap();
            CreateMap<GetDiscountsResponce, IEnumerable<Bonus>>()
                .ReverseMap();

            CreateMap<Bonus, Discount>()
                .ReverseMap();
            CreateMap<Discount, Bonus>()
                .ReverseMap();

            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));
        }

    }

    public class DataAccessProfile3 : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="DataAccessProfile" /> class.</summary>
        public DataAccessProfile3()
        {
            CreateMap<PersonalProfile, PersonalData>()
                .ForMember(dest => dest.PersonalId,
                    opt =>
                        opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.PhoneNumber,
                    opt =>
                    opt.MapFrom(src => src.Phone));


            CreateMap<PersonalData, PersonalProfile>()
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.PersonalId.ToString()))
                .ForMember(dest => dest.Phone,
                    opt =>
                        opt.MapFrom(src => src.PhoneNumber));

            //CreateMap<IEnumerable<Bonus>, IEnumerable<Discount>>()
            //    .ReverseMap();
            //CreateMap<IEnumerable<Discount>, IEnumerable<Bonus>>()
            //    .ReverseMap();

            //CreateMap<IEnumerable<Bonus>, IEnumerable<Discount>>();
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
                        opt.MapFrom(src => src.Type));

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


            CreateMap<DiscountType, ProfileService.BusinessLogic.DiscountType>().ReverseMap();
            CreateMap<ProfileService.BusinessLogic.DiscountType, DiscountType>().ReverseMap();
        }

    }
}