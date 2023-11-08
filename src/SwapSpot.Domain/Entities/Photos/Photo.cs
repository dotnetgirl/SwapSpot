using SwapSpot.Domain.Commons;

namespace SwapSpot.Domain.Entities.Photos;

public class Photo : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
}