namespace ProfileService.GRPC.Configuration
{
    // TODO: Rename class from AppConfiguration to AppConfigurations
    // TODO: Change file location to ProfileService.Grpc.Infrastructure.Configurations
    /// <summary>
    ///   Add environment variables to <seealso cref="WebApplicationBuilder"/>
    /// </summary>
    public static partial class AppConfiguration
    {
        /// <summary>Adds the application settings.</summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <returns>
        ///   <seealso cref="WebApplicationBuilder"/>
        /// </returns>
        public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Host.ConfigureAppConfiguration(config =>
            {
                var prefix = "ProfileService_";
                config.AddEnvironmentVariables(prefix);
                config.Build();
            });

            return applicationBuilder;
        }
    }
}
