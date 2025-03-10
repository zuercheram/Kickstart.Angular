using Kickstart.Angular.Domain;
using Kickstart.Angular.Domain.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1852 // Seal internal types
await CreateHostBuilder(args).Build().RunAsync();
#pragma warning restore CA1852 // Seal internal types

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddJsonFile("appsettings.json");
            builder.AddCommandLine(args); // must be last
        })
        .ConfigureServices(
            (hostContext, services) =>
            {
                services.Configure<MigrationOptions>(hostContext.Configuration.GetSection("Migration"));

                services.AddLogging(
                    builder =>
                    {
                        builder.AddConsole();
                        builder.AddDebug();
                    }
                );

                services.AddDbContext<ApplicationDbContext>(
                    options =>
                    {
                        MigrationOptions migrationOptions = new();
                        hostContext.Configuration.GetSection("Migration").Bind(migrationOptions);

                        options.UseSqlServer(
                            migrationOptions.ConnectionString,
                            options =>
                            {
                                options.MigrationsAssembly(typeof(MigrationService).Assembly.FullName);
                                options.EnableRetryOnFailure();
                                options.CommandTimeout(600);
                            }
                        );
                    },
                    ServiceLifetime.Singleton,
                    ServiceLifetime.Singleton
                );

                services.AddHostedService<MigrationService>();
            }
        );
}
