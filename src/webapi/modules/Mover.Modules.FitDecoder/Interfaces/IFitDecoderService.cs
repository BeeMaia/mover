namespace Mover.Modules.FitDecoder.Interfaces;

public interface IFitDecoderService
{
    Task<string> DecodeAsync(Guid rawId, string fileName, CancellationToken cancellationToken);
}
