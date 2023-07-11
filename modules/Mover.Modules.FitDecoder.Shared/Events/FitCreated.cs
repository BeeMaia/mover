using Mover.Shared.Models;

namespace Mover.Modules.FitDecoder.Shared.Events;

public record FitCreated : Event
{
    public string FileName { get; }

    public FitCreated(string fileName)
    {
        FileName = fileName;
    }
}
