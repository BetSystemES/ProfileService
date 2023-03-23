using Microsoft.EntityFrameworkCore;
using ProfileService.DataAccess;
using ProfileService.GRPC.Infrastructure.Configuration;
using ProfileService.GRPC.Interceptors;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerialLogger();

var jwtConfig = builder.GetAppSettings<JwtConfig>();

var connectionString = builder.Configuration.GetConnectionString("ProfileDb");
builder.Services.AddPostgreSqlContext(options =>
{
    options.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure(3));
});

// Add services to the container.
builder.Services
    .AddJwtAuthentication(jwtConfig)
    .AddRepositories()
    .AddProviders()
    .AddInfrastructureServices()
    .AddFluentValidation()
    .AddGrpc(options =>
    {
        options.Interceptors.Add<ErrorHandlingInterceptor>();
        options.Interceptors.Add<ValidationInterceptor>();
    });

var app = builder.Build();

app.UseAuthentication();   // добавление middleware аутентификации
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProfileService.GRPC.Services.ProfileService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

namespace ProfileService.Grpc
{
    public partial class Program
    { }
}