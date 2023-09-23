namespace Mover.Shared.Models.GPX;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.topografix.com/GPX/1/1")]
public class GpxMetadataAuthorEmail
{
    [System.Xml.Serialization.XmlAttribute("id")]
    public string? Id { get; set; }
    
    [System.Xml.Serialization.XmlAttribute("domain")]
    public string? Domain { get; set; }
}
