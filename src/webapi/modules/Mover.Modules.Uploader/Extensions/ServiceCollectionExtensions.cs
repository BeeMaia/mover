using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Mover.Modules.Uploader.Handlers.Commands;
using Mover.Modules.Uploader.Interfaces;
using Mover.Modules.Uploader.Services;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Modules.Uploader.Strategies;
using Mover.Shared.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUploaderModule(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IUploaderService, UploaderService>();

        services.AddScoped<IUploadStrategy, FitStrategy>();
        services.AddScoped<IUploadStrategy, GpxStrategy>();
        services.AddScoped<UploadContext>();

        services.AddScoped<ICommandHandler<UploadFile>, UploadFileHandler>();

        services.AddScoped<UploaderOrchestrator>();

        services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
            options.MultipartHeadersLengthLimit = int.MaxValue;
        });

        return services;
    }
}
