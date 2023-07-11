using Mover.Shared.Models;

namespace Mover.Shared.Interfaces;

public interface IServiceBus
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken) where T : Command;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : Event;
}
