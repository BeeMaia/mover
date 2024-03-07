using Mover.Shared.Models;

namespace Mover.FitDecoder.Shared.Commands;

public record DecodeFit : Command
{
    public const string Topic = "decodefit";
    public Guid RawId { get; }

    public string FileName { get; }

    public DecodeFit(Guid rawId, string fileName)
    {
        RawId = rawId;
        FileName = fileName;
    }
}