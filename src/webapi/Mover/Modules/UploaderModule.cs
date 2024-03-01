﻿using Mover.Interfaces;
using Mover.Modules.Uploader;

namespace Mover.Modules;

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
            .DisableAntiforgery();
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddUploaderModule();
    }
}