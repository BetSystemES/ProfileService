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
            CreateMap<PersonalData, PersonalDataRequest>()
                .ReverseMap();
            CreateMap<PersonalData, PersonalDataResponce>()
                .ReverseMap();
            CreateMap<PersonalDataRequest, PersonalData>()
                .ReverseMap();
            CreateMap<PersonalDataResponce, PersonalData>()
                .ReverseMap();
            CreateMap<Guid, string>()
                .ConvertUsing(s => s.ToString());
            CreateMap<string, Guid>()
                .ConvertUsing(s => Guid.Parse(s));
        }
    }
}
