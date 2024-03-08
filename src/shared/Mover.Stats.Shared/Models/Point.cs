using System.Runtime.Serialization;

namespace Mover.Stats.Shared.Models;

public class Point
{
    [DataMember(Name = "ts")]
    public long Timestamp { get; set; }
    [DataMember(Name = "temp")]
    public decimal Temp { get; set; }
    [DataMember(Name = "ele")]
    public decimal Elevation { get;set; }
    [DataMember(Name = "s")]
    public decimal Speed { get; set; }
    [DataMember(Name = "hr")]
    public short HeartRate { get; set; }
    [DataMember(Name = "c")]
    public short Cadence { get; set; }
}