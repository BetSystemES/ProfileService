using FluentValidation;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Services;
using ProfileService.GRPC.Infrastructure.Mappings;

namespace ProfileService.GRPC.Infrastructure.Configuration
{
    public static partial class AppConfigurations
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