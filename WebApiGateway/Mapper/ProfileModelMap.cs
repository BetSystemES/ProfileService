using AutoMapper;
using ProfileService.GRPC;
using WebApiGateway.Models;

namespace WebApiGateway.Mapper
{
    public class ProfileModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileModelMap"/> class.
        /// </summary>
        public ProfileModelMap()
        {
            CreateMap<ProfileModel, UserProfile>()
                .ReverseMap();
        }
    }
}
