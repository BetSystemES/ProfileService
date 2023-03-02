using Serilog;
// TODO: remove unused/sort usings
using Serilog.Sinks.SystemConsole.Themes;


// TODO: remove all empty lines
// TODO: Change file location to ProfileService.GRPC.Infrastructure.Configurations.SeriLog
// TODO: fix namespace to ProfileService.GRPC.Infrastructure.Configurations.SeriLog
namespace ProfileService.GRPC.Configuration
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
