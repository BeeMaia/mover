namespace Mover.Modules.FitDecoder.Interfaces;

public interface IFitDecoderService
{
    Task DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken);
}
