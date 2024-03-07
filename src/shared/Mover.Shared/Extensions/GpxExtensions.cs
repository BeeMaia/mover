using System.Xml.Serialization;
using Mover.Shared.Models.GPX;

namespace Mover.Shared.Extensions;

public static class GpxExtensions
{
    public static byte[] ToArray(this Gpx gpx)
    {
        MemoryStream memoryStream = new();
        XmlSerializer xmlSerializer = new(typeof(Gpx), [typeof(GpxTrkTrkptExtensions)]);
        xmlSerializer.Serialize(memoryStream, gpx);
        memoryStream.Position = 0;

        return memoryStream.ToArray();
    }

    public static Gpx? ToGpx(this byte[] bytes)
    {
        MemoryStream memoryStream = new(bytes);
        memoryStream.Position = 0;
        XmlSerializer xmlSerializer = new(typeof(Gpx), [typeof(GpxTrkTrkptExtensions)]);
        var result = xmlSerializer.Deserialize(memoryStream);

        return result as Gpx;
    }
}
