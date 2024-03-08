using System.Runtime.Serialization;

namespace Mover.Stats.Shared.Models;
public class Activity
{
    [DataMember(Name = "idRaw")]
    public string IdRaw { get; set; }
    [DataMember(Name = "fn")]
    public string FileName { get; set; }
    [DataMember(Name = "activityType")]
    public string ActivityType { get; set; }
    [DataMember(Name = "timestamp")]
    public long Timestamp { get; set; }
    [DataMember(Name = "tTime")]
    public long TotalTime { get; set; }
    [DataMember(Name = "tPDrop")]
    public double TotalPositiveDrop { get; set; }
    [DataMember(Name = "tDistance")]
    public double TotalDistance { get; set; }

    [DataMember(Name = "points")]
    public IEnumerable<Point> Points { get; set; }
}
