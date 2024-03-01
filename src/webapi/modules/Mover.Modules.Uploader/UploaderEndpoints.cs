using Microsoft.AspNetCore.Http;
using Mover.Modules.Uploader.Services;

namespace Mover.Modules.Uploader;

public class UploaderEndpoints
{
    public static async Task<IResult> HandleUploadAsync(IFormFile file, UploaderOrchestrator uploaderOrchestrator, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return Results.BadRequest("File is empty");

        await uploaderOrchestrator.UploadAsync(file, cancellationToken).ConfigureAwait(false);

        return Results.Accepted();
    }
}
