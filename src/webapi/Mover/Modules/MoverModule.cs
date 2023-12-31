﻿using Mover.Interfaces;
using Mover.Shared;
using Mover.Shared.Handlers;
using Mover.Shared.Interfaces;
using Mover.Shared.Repositories;

namespace Mover.Modules;

public sealed class MoverModule : IModule
{
    public bool IsEnabled => true;

    public int Order => 1;

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // no endpoints
    }

    public void MapDispatchers(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/mover/dispatchers")
            .WithTags("MoverDispatchers");

        mapGroup.MapPost("/blobcreated", MoverDispatcher.HandleBlobCreatedAsync)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("BlobCreatedEvent");
    }

    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ValidationHandler>();
        builder.Services.AddScoped<IServiceBus, ServiceBus>();
        builder.Services.AddScoped<IBlobRepository, BlobRepository>();
    }
}
