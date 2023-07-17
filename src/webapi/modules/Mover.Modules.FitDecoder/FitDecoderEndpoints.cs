using Google.Api;
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
            // Set the CORS headers
            http.Response.Headers.Add("WebHook-Allowed-Origin", "*");
            http.Response.Headers.Add("Access-Control-Allow-Methods", "POST");

            // Return a 200 OK response
            http.Response.StatusCode = StatusCodes.Status200OK;

            try
            {
                if (body != null && http.Request.Method.Equals("POST", StringComparison.CurrentCultureIgnoreCase))
                {
                    var fileName = body.data.url.Split('/').Last();
                    await EventDispatcher.DispatchAsync(new FitCreated(fileName), eventHandler, cancellationToken);
                }

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
