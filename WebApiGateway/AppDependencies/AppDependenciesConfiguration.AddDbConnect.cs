using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using ProfileService.DataAccess;
using ProfileService.DataAccess.EF;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static IServiceCollection AddDbConnect(this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("ProfileDb");

            services.AddPostgreSqlContext(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddDbContext<ProfileDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
