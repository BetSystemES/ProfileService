using FluentValidation;
using ProfileService.BusinessLogic;

namespace ProfileService.GRPC.Configuration
{
    public static partial class AppConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DataAccessProfile>();
            });

            services.AddScoped<IProfileService, CustomProfileService>();

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            return services;
        }

    }
}
