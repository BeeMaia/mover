using Microsoft.AspNetCore.Http;
using Mover.Modules.FitDecoder.Shared.Events;
using Mover.Shared.Dispatchers;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder
{
    public static class FitDecoderEndpoints
    {
        public static async Task<IResult> HandleFitCreatedAsync(
            HttpContext http,
            IEventHandler<FitCreated> eventHandler,
            CloudEvent<BlobCreated> body,
            CancellationToken cancellationToken
        )
        {
            if (http.Request.Method.Equals("options", StringComparison.CurrentCultureIgnoreCase))
            {
                http.Response.Headers.Add("WebHook-Allowed-Origin", http.Request.Headers.Origin);
                return Results.NoContent();
            }

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
