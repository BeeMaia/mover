using Microsoft.Extensions.Logging;
using Mover.Shared.Interfaces;
using Mover.Shared.Models;

namespace Mover.Shared.Handlers;

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
{
    protected readonly ILogger Logger;
    protected readonly IServiceBus ServiceBus;

    protected CommandHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus)
    {
        Logger = loggerFactory.CreateLogger(GetType());
        ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}
