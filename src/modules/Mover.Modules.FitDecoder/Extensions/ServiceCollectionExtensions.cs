using Mover.Modules.FitDecoder.Interfaces;
using Mover.Modules.FitDecoder.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFitDecoderModule(this IServiceCollection services)
    {
        services.AddScoped<IFitDecoderService, FitDecoderService>();

        return services;
    }
}
