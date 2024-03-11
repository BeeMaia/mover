using Mover.Stats.Interfaces;
namespace Mover.Stats;

public static class StatsEndpoints
{
    public static async Task<IResult> HandleGetAsync(IStatsService service, CancellationToken cancellationToken)
    {
        var collection = await service.GetAsync(cancellationToken);

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
