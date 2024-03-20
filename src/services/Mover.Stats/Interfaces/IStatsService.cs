using Mover.Stats.ViewModels;

namespace Mover.Stats.Interfaces;

public interface IStatsService
{
    Task<IEnumerable<ActivityVM>> GetAsync(string userId, CancellationToken cancellationToken);
    Task<ActivityWithCoordinatesVM?> GetByIdRawAsync(string idRaw, CancellationToken cancellationToken);
    Task WriteAsync(Guid rawId, string fileName, string userId, CancellationToken cancellationToken);
}
