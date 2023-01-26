using System;
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
            CreateMap<PersonalData, PersonalProfile>()
                .ReverseMap();
            CreateMap<PersonalProfile, PersonalData>()
                .ReverseMap();
          
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
}
