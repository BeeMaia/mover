namespace Mover.Shared.Models;

public record Command
{
    public string Name { get; }

    public DateTime CreationDate { get; }

    public Command()
    {
        Name = GetType().Name;
        CreationDate = DateTime.UtcNow;
    }
}