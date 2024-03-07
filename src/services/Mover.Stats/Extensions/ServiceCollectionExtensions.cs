using FluentValidation.AspNetCore;
using Mover.Shared.Interfaces;
using Mover.Stats.Handlers.Commands;
using Mover.Stats.Handlers.Events;
using Mover.Stats.Interfaces;
using Mover.Stats.Services;
using Mover.Stats.Shared.Commands;
using Mover.Uploader.Shared.Events;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStatsModule(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IStatsService, StatsService>();

        services.AddScoped<ICommandHandler<WriteStats>, WriteStatsHandler>();
        services.AddScoped<IEventHandler<UploadedGpx>, UploadedGpxHandler>();

        return services;
    }
}
