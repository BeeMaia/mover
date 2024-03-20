using Mover.Shared.Interfaces;

namespace Mover.Stats.Modules;

public sealed class StatsModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 1;

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/stats")
            .WithTags("Stats");

        mapGroup.MapGet("", StatsEndpoints.HandleGetAsync)
            .Produces(StatusCodes.Status200OK)
            .WithName("Get").RequireAuthorization();

        mapGroup.MapGet("/{idRaw}", StatsEndpoints.HandleGetByIdRawAsync)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK)
            .WithName("GetById").RequireAuthorization();
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
        builder.Services.AddStatsModule(builder.Configuration);
    }
}
