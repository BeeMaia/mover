using Mover.Stats.Shared.Models;
using Mover.Stats.ViewModels;

namespace Mover.Stats.Interfaces;

public interface IStatsService
{
    Task<IEnumerable<ActivityVM>> GetAsync(CancellationToken cancellationToken);
    Task<Activity?> GetByIdRawAsync(string idRaw, CancellationToken cancellationToken);
    Task WriteAsync(Guid rawId, string fileName, CancellationToken cancellationToken);
}
