using WebApiGateway.Settings;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static ServiceEndpointsSettings AddServiceEndpoints(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Host.ConfigureAppConfiguration(config =>
            {
                var prefix = "Gateway_";
                config.AddEnvironmentVariables(prefix);
                config.Build();
            });

            webApplicationBuilder.Services.AddServiceEndpoints(webApplicationBuilder.Configuration);

            return webApplicationBuilder.Configuration
                                        .GetRequiredSection("ServiceEndpoints")
                                        .Get<ServiceEndpointsSettings>();
        }

        private static void AddServiceEndpoints(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .Configure<ServiceEndpointsSettings>(configuration.GetSection("ServiceEndpoints"));
        }
    }
}
