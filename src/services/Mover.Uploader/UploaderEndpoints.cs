using Mover.Uploader.Services;

namespace Mover.Uploader;

public class UploaderEndpoints
{
    public static async Task<IResult> HandleUploadAsync(IFormFile file, UploaderOrchestrator uploaderOrchestrator, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return Results.BadRequest("File is empty");

        await uploaderOrchestrator.UploadAsync(file, cancellationToken);

        return Results.Accepted();
    }
}
