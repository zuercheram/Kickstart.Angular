using Azure.Identity;
using Kickstart.Angular.Server.Extensions;
using Serilog;

#pragma warning disable CA1305
#pragma warning disable CA1852 // Seal internal types
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
#pragma warning restore CA1852 // Seal internal types
#pragma warning restore CA1305

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost
        .ConfigureKestrel(serverOptions => { serverOptions.AddServerHeader = false; })
        .ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            var config = configurationBuilder.Build();
            var azureKeyVaultEndpoint = config["AzureKeyVaultEndpoint"];
            if (!string.IsNullOrEmpty(azureKeyVaultEndpoint))
            {
                // Add Secrets from KeyVault
                Log.Information("Use secrets from {AzureKeyVaultEndpoint}", azureKeyVaultEndpoint);
                configurationBuilder.AddAzureKeyVault(new Uri(azureKeyVaultEndpoint), new DefaultAzureCredential());
            }
            else
            {
                // Add Secrets from UserSecrets for local development
                configurationBuilder.AddUserSecrets("6EEF2A8F-B698-4194-9841-BBD25460D364");
            }
        });

    builder.Host.UseSerilog((context, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(context.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    await app.RunAsync();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException"
    && ex.GetType().Name is not "HostAbortedException")
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    await Log.CloseAndFlushAsync();
}
