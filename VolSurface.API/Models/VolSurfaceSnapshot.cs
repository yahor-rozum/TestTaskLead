namespace VolSurface.API.Models;

public class VolSurfaceSnapshot
{
    public string Underlying { get; set; } = string.Empty;
    public DateTime AsOf { get; set; }
    public List<VolSurfacePoint> Points { get; set; } = new();
}
