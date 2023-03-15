using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using ProfileService.Grpc;

namespace ProfileService.FunctionalTests
{
    public class GrpcAppFactory : WebApplicationFactory<Program>
    {
        /// <summary>Gives a fixture an opportunity to configure the application before it gets built.</summary>
        /// <param name="builder">The <see cref="T:Microsoft.AspNetCore.Hosting.IWebHostBuilder">IWebHostBuilder</see> for the application.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("Environment", "IntegrationTest");
            base.ConfigureWebHost(builder);
        }
    }
}