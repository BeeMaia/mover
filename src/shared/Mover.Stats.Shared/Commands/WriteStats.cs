using Mover.Shared.Models;

namespace Mover.Stats.Shared.Commands;

public record WriteStats : Command
{
    public const string Topic = "writestats";
    public string FileName { get; }
    public Guid RawId { get; }

    public WriteStats(Guid rawId, string fileName)
    {
        FileName = fileName;
        RawId = rawId;
    }
}
