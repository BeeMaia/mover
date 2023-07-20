using Mover.Shared.Models.GPX;
using System.Xml.Serialization;

namespace Mover.Shared.Extensions
{
    public static class GpxExtensions
    {
        public static byte[] ToArray(this Gpx gpx)
        {
            MemoryStream memoryStream = new();
            XmlSerializer xmlSerializer = new(typeof(Gpx));
            xmlSerializer.Serialize(memoryStream, gpx);
            memoryStream.Position = 0;

            return memoryStream.ToArray();
        }
    }
}
