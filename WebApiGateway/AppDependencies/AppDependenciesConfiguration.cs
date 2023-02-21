namespace WebApiGateway.AppDependencies
{
    public static partial class AppDependenciesConfiguration
    {
        public static void ConfigureDependencies(this WebApplicationBuilder builder)
        {
            var appSettings = builder.AddServiceEndpoints();
            var configuration = builder.Configuration;

            builder
                .Services
                .AddHttpContextAccessor()
                .AddSwagger()
                .AddGrpcClients(appSettings);
        }
    }
}
