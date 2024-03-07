namespace Mover.Stats.Shared.Models;
public class Track
{
    public Guid IdRaw { get; set; }
    public string FileName { get; set; }
    public string ActivityType { get; set; }
    public long Timestamp { get; set; }

    public IEnumerable<Point> Points { get; set; }
}
