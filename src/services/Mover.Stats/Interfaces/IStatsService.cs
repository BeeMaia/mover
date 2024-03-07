namespace Mover.Stats.Interfaces;

public interface IStatsService
{
    Task WriteAsync(Guid rawId, string fileName, CancellationToken cancellationToken);
}
