using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Mover.Shared.Interfaces;
using Mover.Uploader.Handlers.Commands;
using Mover.Uploader.Interfaces;
using Mover.Uploader.Services;
using Mover.Uploader.Shared.Commands;
using Mover.Uploader.Strategies;

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
