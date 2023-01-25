using Microsoft.EntityFrameworkCore;
using ProfileService.DataAccess.EF;
using ProfileService.DataAccess.Interfaces;

namespace ProfileService.GRPC.Configuration
{
    

    //public static IServiceCollection AddPostgreSqlContext(this IServiceCollection services,
    //    Action<DbContextOptionsBuilder> options)
    //{
    //    services.AddDbContextPool<ProfileContext>(options);

    //    services.AddScoped<IDataContext, UserProfileContext>();

    //    services.AddScoped(serviceProvider =>
    //        serviceProvider.GetRequiredService<ProfileContext>()
    //            .Set<Bonus>());
    //    services.AddScoped(serviceProvider =>
    //        serviceProvider.GetRequiredService<ProfileContext>()
    //            .Set<PersonalData>());

    //    return services;
    //}
}
