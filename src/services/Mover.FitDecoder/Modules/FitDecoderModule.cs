using Mover.Shared.Interfaces;

namespace Mover.FitDecoder.Modules;

public sealed class FitDecoderModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 2;

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/fitDecoder/dispatchers")
            .WithTags("FitDecoderDispatchers");

        mapGroup.MapPost("/decodefit", FitDecoderDispatcher.HandleDecodeFitAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("DecodeFitCommand");

        mapGroup.MapPost("/uploadedfit", FitDecoderDispatcher.HandleUploadedFitAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("UploadedFitEvent");
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSharedServices(builder.Configuration);
        builder.Services.AddFitDecoderModule();
    }
}
