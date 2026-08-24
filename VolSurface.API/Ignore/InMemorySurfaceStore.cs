// Do not edit anything in this folder — see _README.txt.
// Stands in for a real database.

namespace VolSurface.API.Ignore;

using VolSurface.API.Models;

public class InMemorySurfaceStore
{
    private static readonly Dictionary<string, VolSurfaceSnapshot> Snapshots = new()
    {
        ["EURUSD"] = new VolSurfaceSnapshot
        {
            Underlying = "EURUSD",
            AsOf = DateTime.UtcNow,
            Points = new List<VolSurfacePoint>
            {
                new() { Tenor = "1M", Strike = 1.10, Vol = 0.072 },
                new() { Tenor = "3M", Strike = 1.10, Vol = 0.081 },
                new() { Tenor = "1Y", Strike = 1.10, Vol = 0.095 },
            }
        },
        ["USDJPY"] = new VolSurfaceSnapshot
        {
            Underlying = "USDJPY",
            AsOf = DateTime.UtcNow,
            Points = new List<VolSurfacePoint>
            {
                new() { Tenor = "1M", Strike = 148.0, Vol = 0.091 },
                new() { Tenor = "3M", Strike = 148.0, Vol = 0.099 },
                new() { Tenor = "1Y", Strike = 148.0, Vol = 0.112 },
            }
        },
        ["EURGBP"] = new VolSurfaceSnapshot
        {
            Underlying = "EURGBP",
            AsOf = DateTime.UtcNow,
            Points = new List<VolSurfacePoint>
            {
                new() { Tenor = "1M", Strike = 0.86, Vol = 0.068 },
                new() { Tenor = "3M", Strike = 0.86, Vol = 0.075 },
                new() { Tenor = "1Y", Strike = 0.86, Vol = 0.089 },
            }
        }
    };

    public VolSurfaceSnapshot Get(string underlying)
    {
        return Snapshots[underlying];
    }

    public Task SaveAsync(string underlying, VolSurfaceSnapshot snapshot)
    {
        Snapshots[underlying] = snapshot;
        return Task.CompletedTask;
    }
}
