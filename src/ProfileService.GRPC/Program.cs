using Microsoft.EntityFrameworkCore;
using ProfileService.GRPC.Services;
using ProfileService.DataAccess;
using Npgsql;
using ProfileService.DataAccess.EF;
using ProfileService.GRPC.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682


var connectionString =  builder.Configuration.GetSection("PostgreSql:ConnectionString").ToString();
var password = builder.Configuration.GetSection("PostgreSql:DbPassword");

builder.Services.AddPostgreSqlContext(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<ProfileContext>(options => options.UseNpgsql(connectionString));


// Add services to the container.
builder.Services
    .AddRepositories()
    .AddProviders()
    .AddInfrastructureServices()
    .AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProfileService.GRPC.Services.ProfileService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
