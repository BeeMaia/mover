using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Mover.Shared;
using Mover.Shared.Handlers;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;
using Mover.Shared.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ValidationHandler>();
        services.AddScoped<IServiceBus, ServiceBus>();
        services.AddScoped<IBlobRepository, BlobRepository>()
            .Configure<BlobOptions>(configuration.GetSection(nameof(BlobOptions)));

        return services;
    }

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        var store = builder.Configuration.GetValue<string>(Constants.Options.SecretsStore);
        if (!string.IsNullOrEmpty(store))
        {
            builder.Configuration.AddDaprSecretStore(store, new DaprClientBuilder().Build());
        }
    }
}
