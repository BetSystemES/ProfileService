using AutoMapper;
using ProfileService.GRPC;
using WebApiGateway.Models;

namespace WebApiGateway.Mapper
{
    public class DiscountModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscountModelMap"/> class.
        /// </summary>
        public DiscountModelMap()
        {
            CreateMap<DiscountModelType, DiscountType>()
                .ReverseMap();

            CreateMap<DiscountModel, Discount>()
                .ReverseMap();

            CreateMap<IEnumerable<DiscountModel>, IEnumerable<Discount>>();
        }
    }
}
