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

        mapGroup.MapPost("/fitcreated", FitDecoderDispatcher.HandleFitCreatedAsync)
              .Produces(StatusCodes.Status204NoContent)
              .WithName("FitCreatedEvent");
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddFitDecoderModule();
    }
}
