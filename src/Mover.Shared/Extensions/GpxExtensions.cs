using Mover.Shared.Models.GPX;
using System.Xml.Serialization;

namespace Mover.Shared.Extensions
{
    public static class GpxExtensions
    {
        public static byte[] ToArray(this Gpx gpx)
        {
            var serializer = new XmlSerializer(typeof(Gpx));

            var memStream = new MemoryStream();
            using (var writer = new StreamWriter(memStream))
            {
                serializer.Serialize(writer, gpx);
            }

            memStream.Position = 0;

            return memStream.ToArray();
        }
    }
}
