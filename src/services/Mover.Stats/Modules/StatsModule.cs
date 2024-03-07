using Mover.Shared.Interfaces;

namespace Mover.Stats.Modules;

public sealed class StatsModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 1;

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/stats/dispatchers")
           .WithTags("StatsDispatchers");

        mapGroup.MapPost("/uploadedgpx", StatsDispatcher.HandleUploadedGpxAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("UploadedGpxEvent");

        mapGroup.MapPost("/writestats", StatsDispatcher.HandleWriteStatsAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("WriteStatsCommand");
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSharedServices(builder.Configuration);
        builder.Services.AddStatsModule();
    }
}
