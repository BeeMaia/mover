using FluentValidation.AspNetCore;
using Mover.Modules.FitDecoder.Handlers.Commands;
using Mover.Modules.FitDecoder.Handlers.Events;
using Mover.Modules.FitDecoder.Interfaces;
using Mover.Modules.FitDecoder.Services;
using Mover.Modules.FitDecoder.Shared.Commands;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFitDecoderModule(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IFitDecoderService, FitDecoderService>();

        services.AddScoped<ICommandHandler<DecodeFit>, DecodeFitHandler>();
        services.AddScoped<IEventHandler<FitCreated>, FitCreatedHandler>();

        return services;
    }
}
