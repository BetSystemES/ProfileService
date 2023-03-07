using FluentValidation;

namespace ProfileService.GRPC.Infrastructure.Configuration
{
    public static partial class AppConfigurations
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            return services;
        }
    }
}