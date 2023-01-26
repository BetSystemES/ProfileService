
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfileService.BusinessLogic;
using ProfileService.DataAccess.EF;
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
            services.AddDbContextPool<ProfileContext>(options);

            services.AddScoped<IDataContext, UserProfileContext>();

            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<ProfileContext>()
                    .Set<Bonus>());
            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<ProfileContext>()
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
                .AddScoped<IRepository<PersonalData>, ProfilesRepositiry>()
                .AddScoped<IRepository<Bonus>, BonusesRepositiry>();
            return services;
        }

        /// <summary>Register providers in service collection.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IProvider<Bonus>, BonusProvider>();

            return services;
        }


    }
}
