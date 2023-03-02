// TODO: remove unused/sort usings
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfileService.BusinessLogic;
using ProfileService.DataAccess.EF;
using ProfileService.DataAccess.Providers;
using ProfileService.DataAccess.Repositories;
using ProfileService.EntityModels.Models;


// TODO: remove all empty lines
namespace ProfileService.DataAccess
{
    // TODO: change file location to ProfileService.DataAccess.Extensions
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
                    .Set<PersonalData>());

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
                .AddScoped<IRepository<PersonalData>, ProfilesRepository>()
                .AddScoped<IRepository<Bonus>, BonusesRepository>();
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
            services.AddScoped<IProvider<PersonalData>, PersonalDataProvider>();

            return services;
        }


    }
}
