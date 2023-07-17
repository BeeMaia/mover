using Mover.Interfaces;
using Mover.Modules.FitDecoder;

namespace Mover.Modules;

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
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/fitDecoder")
            .WithTags("FitDecoder");

        mapGroup.MapPost("/fitcreated", FitDecoderEndpoints.HandleFitCreatedAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("FitCreatedHook");
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddFitDecoderModule();
    }
}
