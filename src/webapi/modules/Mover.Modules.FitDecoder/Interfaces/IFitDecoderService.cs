using Mover.Shared.Models.GPX;

namespace Mover.Modules.FitDecoder.Interfaces;

public interface IFitDecoderService
{
    Task<Gpx> DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken);
}
