using Microsoft.AspNetCore.Http;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared.Dispatchers;

public static class EventDispatcher
{
    public static async Task<IResult> DispatchAsync<TEvent>(TEvent @event, IEventHandler<TEvent> eventHandler, CancellationToken cancellationToken) where TEvent : Event
    {
        await eventHandler.HandleAsync(@event, cancellationToken).ConfigureAwait(false);
        return Results.NoContent();
    }
}