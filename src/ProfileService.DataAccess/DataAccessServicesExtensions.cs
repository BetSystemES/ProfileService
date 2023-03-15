using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;
using ProfileService.DataAccess.Providers;
using ProfileService.DataAccess.Repositories;


namespace ProfileService.DataAccess
{
    public static class DataAccessServicesExtensions
    {
        /// <summary>Register the PostgreSql context.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddPostgreSqlContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<ProfileDbContext>(options);

            services.AddScoped<IDataContext, ProfileDataContext>();

            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<ProfileDbContext>()
                    .Set<Bonus>());
            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<ProfileDbContext>()
                    .Set<ProfileData>());

            return services;
        }

        /// <summary>Register repositories in service collection.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IProfileRepository, ProfilesRepository>()
                .AddScoped<IBonusRepository, BonusesRepository>();
            return services;
        }

        /// <summary>Register providers in service collection.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IFinder<Bonus>, BonusFinder>();
            services.AddScoped<IProvider<Bonus>, BonusFinder>();
            services.AddScoped<IProvider<ProfileData>, ProfileDataProvider>();

            return services;
        }


    }
}
