using FluentValidation.AspNetCore;
using MongoDB.Driver;
using Mover.Shared;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;
using Mover.Stats.Handlers.Commands;
using Mover.Stats.Handlers.Events;
using Mover.Stats.Interfaces;
using Mover.Stats.Repositories;
using Mover.Stats.Services;
using Mover.Stats.Shared.Commands;
using Mover.Uploader.Shared.Events;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IStatsService, StatsService>();

        services.AddScoped<ICommandHandler<WriteStats>, WriteStatsHandler>();
        services.AddScoped<IEventHandler<UploadedGpx>, UploadedGpxHandler>();

        services.AddScoped<ActivityRepository>().Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

        services.AddSingleton<IMongoClient>(s =>
            new MongoClient(configuration.GetValue<string>(Constants.Secrets.MoverDbConnString))
        );

        return services;
    }
}
