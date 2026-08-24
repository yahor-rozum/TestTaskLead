namespace VolSurface.API.Models;

public class VolSurfacePoint
{
    public string Tenor { get; set; } = string.Empty;
    public double Strike { get; set; }
    public double Vol { get; set; }
}
