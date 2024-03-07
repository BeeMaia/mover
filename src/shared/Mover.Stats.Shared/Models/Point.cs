namespace Mover.Stats.Shared.Models;

public class Point
{
    public long Timestamp { get; set; }
    public decimal Temp { get; set; }
    public decimal Speed { get; set; }
    public short HeartRate { get; set; }
    public short Cadence { get; set; }
}