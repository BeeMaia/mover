namespace Mover.Shared.Models;

public class CloudEvent<T>
{
    public string id { get; set; }
    public string source { get; set; }
    public string specversion { get; set; }
    public string type { get; set; }
    public T data { get; set; }
}
