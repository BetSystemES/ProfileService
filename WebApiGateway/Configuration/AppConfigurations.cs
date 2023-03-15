using WebApiGateway.Mapper;

namespace WebApiGateway.Configuration
{
    public static partial class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<ProfileModelMap>();
                config.AddProfile<DiscountModelMap>();
            });

            return services;
        }
    }
}