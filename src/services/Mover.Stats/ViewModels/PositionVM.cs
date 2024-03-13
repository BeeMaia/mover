using System.Text.Json.Serialization;
using Mover.Stats.Shared.Models;

namespace Mover.Stats.ViewModels;

public class PositionVM
{
    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("lng")]
    public decimal Longitude { get; set; }
}

public class PointVM
{
    [JsonPropertyName("ts")]
    public long Timestamp { get; set; }
    [JsonPropertyName("temp")]
    public decimal Temp { get; set; }
    [JsonPropertyName("ele")]
    public decimal Elevation { get; set; }
    [JsonPropertyName("s")]
    public decimal Speed { get; set; }
    [JsonPropertyName("hr")]
    public short HeartRate { get; set; }
    [JsonPropertyName("c")]
    public short Cadence { get; set; }

    public static explicit operator PointVM(Point p)
    {
        return new PointVM
        {
            Timestamp = p.Timestamp,
            Cadence = p.Cadence,
            Speed = p.Speed,
            HeartRate = p.HeartRate,
            Elevation = p.Elevation,
            Temp = p.Temp
        };
    }
}