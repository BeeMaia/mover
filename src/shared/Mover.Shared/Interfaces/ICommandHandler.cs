using Mover.Shared.Models;

namespace Mover.Shared.Interfaces;

public interface ICommandHandler<in TCommand>
    where TCommand : Command
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}