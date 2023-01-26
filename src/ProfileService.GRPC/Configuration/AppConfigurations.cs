using ProfileService.BusinessLogic;

namespace ProfileService.GRPC.Configuration
{
    public static class AppConfigurations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DataAccessProfile>();
            });

            services.AddScoped<IProfileService, CustomProfileService>();

            return services;
        }

    }
}
