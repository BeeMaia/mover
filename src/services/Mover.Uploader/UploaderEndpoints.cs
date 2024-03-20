using System.Security.Claims;
using Mover.Uploader.Services;

namespace Mover.Uploader;

public static class UploaderEndpoints
{
    public static async Task<IResult> HandleUploadAsync(HttpContext httpContext, IFormFile file, UploaderOrchestrator uploaderOrchestrator, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return Results.BadRequest("File is empty");

        var claim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        var rawId = await uploaderOrchestrator.UploadAsync(file, claim?.Value, cancellationToken);

        return Results.Accepted(value: rawId);
    }
}
