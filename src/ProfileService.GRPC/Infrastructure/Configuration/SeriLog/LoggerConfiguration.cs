using Serilog;

namespace ProfileService.GRPC.Infrastructure.Configuration.SeriLog
{
    public static class LoggerConfiguration
    {
        /// <summary>Adds the serilog logger to service collection.</summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <returns>
        ///   The application builder.
        /// </returns>
        public static WebApplicationBuilder AddSerialLogger(this WebApplicationBuilder appBuilder)
        {
            appBuilder.Host.UseSerilog((_, serviceProvider, config) =>
            {
                config = config.WriteTo.Console();
                config = appBuilder.Environment.IsDevelopment()
                    ? config.MinimumLevel.Debug()
                    : config.MinimumLevel.Warning();
            });

            return appBuilder;
        }
    }
}