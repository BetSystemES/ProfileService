// TODO: remove unused/sort usings
using System;
using System.CommandLine;
using ProfileService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProfileService.DatabaseMigrator.Extensions;
using ProfileService.DataAccess.EF;

var rootCommand = new RootCommand("Migrate database by connection string via EntityFramework");
var connectionStringSourceOption = new Option<string>("--connection-string-source",
    "Connection String Source: Available options: env, option");
var connectionStringOption = new Option<string>("--connection-string",
    "Connection string for connect to database");
var connectionStringEnvVariableName = new Option<string>("--connection-string-env-variable-name",
    "Name of env variable which contains the connection string");

rootCommand.AddOption(connectionStringSourceOption);
rootCommand.AddOption(connectionStringOption);
rootCommand.AddOption(connectionStringEnvVariableName);

rootCommand.SetHandler<string, string, string>(MigrateDatabase,
    connectionStringSourceOption,
    connectionStringOption,
    connectionStringEnvVariableName);

return await rootCommand.InvokeAsync(args);

static void MigrateDatabase(string source, string connection, string envName)
{
    if (source != "env" && source != "option")
    {
        throw new InvalidDataException("Invalid value for '--connection-string-source' option");
    }

    var connectionString = source == "option"
        ? connection
        : Environment.GetEnvironmentVariable(envName);

    MigratePostgreSqlServer(connectionString!);
}

static void Migrate<TContext>(Func<IServiceCollection, IServiceCollection> configure)
    where TContext : DbContext
{
    var services = new ServiceCollection()
                        .AddLogging(configure => configure.AddConsole());

    configure(services)
        .BuildServiceProvider()
        .MigrateDatabaseFromContext<TContext>();
}

static void MigratePostgreSqlServer(string connectionString)
{
    Migrate<ProfileDbContext>(services =>
        services.AddPostgreSqlContext(options =>
        {
            options.UseNpgsql(connectionString);
        }));
}
