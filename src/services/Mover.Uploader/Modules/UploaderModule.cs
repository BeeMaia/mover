using Mover.Shared.Interfaces;

namespace Mover.Uploader.Modules;

public sealed class UploaderModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 2;

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/uploader/dispatchers")
            .WithTags("UploaderDispatchers");

        mapGroup.MapPost("/uploadfile", UploaderDispatcher.HandleUploadFileAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("UploadFileCommand");
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/uploader")
            .WithTags("Uploader");

        mapGroup.MapPost("/upload", UploaderEndpoints.HandleUploadAsync)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("UploadFile")
            .DisableAntiforgery().RequireAuthorization();
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSharedServices(builder.Configuration);
        builder.Services.AddUploaderModule();
    }
}