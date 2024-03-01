using Microsoft.AspNetCore.Http;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared.Dispatchers;

public static class CommandDispatcher
{
    public static async Task<IResult> DispatchAsync<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler, CancellationToken cancellationToken) where TCommand : Command
    {
        await commandHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
        return Results.NoContent();
    }
}
