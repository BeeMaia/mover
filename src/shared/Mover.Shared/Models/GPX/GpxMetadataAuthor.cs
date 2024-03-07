namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxMetadataAuthor
{
    [System.Xml.Serialization.XmlElement("name")]
    public string? Name { get; set; }

    [System.Xml.Serialization.XmlElement("email")]
    public GpxMetadataAuthorEmail? Email { get; set; }

    [System.Xml.Serialization.XmlElement("link")]
    public GpxLink? Link { get; set; }
}
