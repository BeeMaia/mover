using Mover.Shared.Models;

namespace Mover.Shared.Interfaces;

public interface IEventHandler<in TEvent>
    where TEvent : Event
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}