using System.Text.Json.Serialization;
using Mover.Stats.Shared.Models;

namespace Mover.Stats.ViewModels;

public class ActivityVM
{
    [JsonPropertyName("idRaw")]
    public string IdRaw { get; set; }

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

    public static explicit operator ActivityVM(Activity a)
    {
        return new ActivityVM
        {
            IdRaw = a.IdRaw,
            ActivityType = a.ActivityType,
            Timestamp = a.Timestamp,
            TotalTime = a.TotalTime,
            TotalPositiveDrop = a.TotalPositiveDrop,
            TotalDistance = a.TotalDistance
        };
    }
}
