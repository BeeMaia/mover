using Mover.Shared.Models;
using Mover.Uploader.Interfaces;

namespace Mover.Uploader.Strategies;

public class UploadContext
{
    private readonly IEnumerable<IUploadStrategy> strategies;

    public UploadContext(IEnumerable<IUploadStrategy> strategies)
    {
        this.strategies = strategies;
    }

    public async Task<Event?> UploadAsync(Guid rawId, string fileName, string userId, byte[] content, CancellationToken cancellationToken)
    {
        var instance = strategies.FirstOrDefault(x => x.Extension.Equals(Path.GetExtension(fileName), StringComparison.InvariantCultureIgnoreCase));

        if (instance is not null)
        {
            return await instance.UploadAsync(rawId, fileName, userId, content, cancellationToken);
        }

        return null;
    }
}
