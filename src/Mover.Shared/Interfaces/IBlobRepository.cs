namespace Mover.Shared.Interfaces;

public interface IBlobRepository
{
    Task CreateBlobAsync(string blobStorage, string fileName, byte[] data, CancellationToken cancellationToken);
    Task<byte[]> GetBlobAsync(string blobStorage, string fileName, CancellationToken cancellationToken);
}