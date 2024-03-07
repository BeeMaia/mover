using FluentValidation.AspNetCore;
using Mover.FitDecoder.Handlers.Commands;
using Mover.FitDecoder.Handlers.Events;
using Mover.FitDecoder.Interfaces;
using Mover.FitDecoder.Services;
using Mover.FitDecoder.Shared.Commands;
using Mover.Shared.Interfaces;
using Mover.Uploader.Shared.Events;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFitDecoderModule(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IFitDecoderService, FitDecoderService>();

        services.AddScoped<ICommandHandler<DecodeFit>, DecodeFitHandler>();
        services.AddScoped<IEventHandler<UploadedFit>, UploadedFitHandler>();

        return services;
    }
}
