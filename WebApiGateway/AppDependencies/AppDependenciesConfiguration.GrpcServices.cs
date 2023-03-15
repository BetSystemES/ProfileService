using WebApiGateway.Settings;
using static ProfileService.GRPC.ProfileService;

namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        private static IServiceCollection AddGrpcClients(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings)
        {
            services
                .AddProfileClient(serviceEndpointsSettings);

            return services;
        }

        private static IServiceCollection AddProfileClient(this IServiceCollection services,
            ServiceEndpointsSettings serviceEndpointsSettings)
        {
            var endpoint = serviceEndpointsSettings.ProfileService;

            return services
                .AddGrcpServiceClient<ProfileServiceClient>("ProfileGrpcClient", endpoint);
        }

        private static IServiceCollection AddGrcpServiceClient<TClient>(this IServiceCollection services, string clientName, string? endpoint) where TClient : class
        {
            return services
                .AddGrpcClient<TClient>(clientName, options =>
                {
                    options.Address = new Uri(endpoint);
                })
                .Services;
        }
    }
}