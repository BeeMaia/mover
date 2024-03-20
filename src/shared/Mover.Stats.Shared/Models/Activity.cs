using System.Text.Json.Serialization;

namespace Mover.Stats.Shared.Models;
public class Activity
{
    [JsonPropertyName("idRaw")]
    public string IdRaw { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("fn")]
    public string FileName { get; set; }

    [JsonPropertyName("activityType")]
    public string ActivityType { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("tTime")]
    public long TotalTime { get; set; }

    [JsonPropertyName("tPDrop")]
    public double TotalPositiveDrop { get; set; }

    [JsonPropertyName("tDistance")]
    public double TotalDistance { get; set; }

    [JsonPropertyName("points")]
    public IEnumerable<Point> Points { get; set; }
}
