using Mover.Shared.Interfaces;
using Mover.Stats.Interfaces;
using Mover.Stats.Shared.Commands;
using Mover.Stats.Shared.Events;

namespace Mover.Stats.Handlers.Commands;

public sealed class WriteStatsHandler : Mover.Shared.Handlers.CommandHandler<WriteStats>
{
    private readonly IStatsService statsService;

    public WriteStatsHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus, IStatsService statsService) : base(loggerFactory, serviceBus)
    {
        this.statsService = statsService ?? throw new ArgumentNullException(nameof(statsService));
    }

    public override async Task HandleAsync(WriteStats command, CancellationToken cancellationToken)
    {
        await statsService.WriteAsync(command.RawId, command.FileName, command.UserId, cancellationToken);

        await ServiceBus.PublishAsync(new WroteStats(command.RawId), cancellationToken);
    }
}
