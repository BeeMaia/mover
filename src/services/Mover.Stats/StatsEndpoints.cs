using System.Security.Claims;
using Mover.Stats.Interfaces;
namespace Mover.Stats;

public static class StatsEndpoints
{
    public static async Task<IResult> HandleGetAsync(HttpContext httpContext, IStatsService service, CancellationToken cancellationToken)
    {
        var claim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        var collection = await service.GetAsync(claim?.Value, cancellationToken);

        return Results.Ok(collection);
    }

    public static async Task<IResult> HandleGetByIdRawAsync(string idRaw, IStatsService service, CancellationToken cancellationToken)
    {
        var activity = await service.GetByIdRawAsync(idRaw, cancellationToken);
        if (activity == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(activity);
    }
}
