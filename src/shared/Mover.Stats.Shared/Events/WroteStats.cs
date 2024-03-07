using Mover.Shared.Models;

namespace Mover.Stats.Shared.Events;
public record WroteStats : Event
{
    public const string Topic = "wrotestats";

    public Guid RawId { get; }

    public WroteStats(Guid rawId)
    {
        RawId = rawId;
    }
}
