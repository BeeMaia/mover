namespace Mover.Shared.Models;

public record Event
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreationDate { get; }

    public Event()
    {
        Id = Guid.NewGuid();
        Name = GetType().Name;
        CreationDate = DateTime.UtcNow;
    }
}
