using Microsoft.Extensions.Logging;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared.Handlers;

public abstract class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : Event
{
    protected readonly ILogger Logger;

    protected EventHandler(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }

    public abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
