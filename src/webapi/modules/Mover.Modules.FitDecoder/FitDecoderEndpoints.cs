﻿using Microsoft.AspNetCore.Http;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder
{
    public static class FitDecoderEndpoints
    {
        public static async Task<IResult> HandleFitCreatedAsync(
            IEventHandler<FitCreated> eventHandler,
            EventGridEvent<BlobCreated> body,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var fileName = body.data.url.Split('/').Last();
                await EventDispatcher.DispatchAsync(new FitCreated(fileName), eventHandler, cancellationToken);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
