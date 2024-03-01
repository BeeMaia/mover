using Microsoft.AspNetCore.Http;
using Mover.Modules.Uploader.Shared.Commands;
using Mover.Shared.Interfaces;

namespace Mover.Modules.Uploader.Services;

public class UploaderOrchestrator
{
    private readonly IServiceBus serviceBus;

    public UploaderOrchestrator(IServiceBus serviceBus)
    {
        this.serviceBus = serviceBus;
    }

    public async Task UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var fileName = file.FileName;

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);
        stream.Position = 0;
        var content = stream.ToArray();

        await serviceBus.SendAsync(new UploadFile(fileName, content), cancellationToken).ConfigureAwait(false);
    }
}