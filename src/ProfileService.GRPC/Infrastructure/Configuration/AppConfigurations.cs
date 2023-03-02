using FluentValidation;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Services;
using ProfileService.GRPC.Infrastructure.Configuration;

namespace ProfileService.GRPC.Configuration
{
    // TODO: Rename class from AppConfiguration to AppConfigurations
    // TODO: Change file location to ProfileService.Grpc.Infrastructure.Configurations
    public static partial class AppConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DataAccessProfile>();
            });

            services.AddScoped<IProfileService, CustomProfileService>();

            // TODO: Add new AppConfigurations partial class for fluent validation
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            return services;
        }

    }
}
