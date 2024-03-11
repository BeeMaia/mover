using System.Text.Json.Serialization;

namespace Mover.Stats.Shared.Models;

public class Point
{
    [JsonPropertyName("ts")]
    public long Timestamp { get; set; }
    [JsonPropertyName("temp")]
    public decimal Temp { get; set; }
    [JsonPropertyName("ele")]
    public decimal Elevation { get;set; }
    [JsonPropertyName("s")]
    public decimal Speed { get; set; }
    [JsonPropertyName("hr")]
    public short HeartRate { get; set; }
    [JsonPropertyName("c")]
    public short Cadence { get; set; }
}