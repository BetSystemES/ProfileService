namespace ProfileService.GRPC.Infrastructure.Configuration
{
    public static partial class AppDependenciesConfiguration
    {
        public static void ConfigureAppSettings<T>(this IServiceCollection services,
            IConfiguration configuration, string? sectionName = null) where T : class
        {
            services.Configure<T>(
                configuration.GetSection(string.IsNullOrEmpty(sectionName) ? typeof(T).Name : sectionName));
        }

        public static T GetAppSettings<T>(this WebApplicationBuilder webApplicationBuilder) where T : class
        {
            return webApplicationBuilder.Configuration
                .GetRequiredSection(typeof(T).Name)
                .Get<T>();
        }
    }
}