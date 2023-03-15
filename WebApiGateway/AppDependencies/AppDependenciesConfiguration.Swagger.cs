using Microsoft.OpenApi.Models;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Gateway - API",
                    Version = "v1",
                    Description = "Documentation of API"
                });

                setup.EnableAnnotations();
            });

            return services;
        }
    }
}