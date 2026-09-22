namespace VolSurface.API.Services;

using VolSurface.API.Ignore;
using VolSurface.API.Models;

public class VolSurfaceService
{
    private static readonly ILogger Logger =
        LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<VolSurfaceService>();

    private readonly InMemorySurfaceStore _store;
    private readonly MarketDataClient _marketDataClient;

    public VolSurfaceService()
    {
        _store = new InMemorySurfaceStore();
        _marketDataClient = new MarketDataClient();
    }

    /// <summary>
    /// Retrieves the volitility surface for the given underlying.
    /// </summary>
    public VolSurfaceSnapshot GetSurface(string underlying)
    {
        return _store.Get(underlying);
    }

    public void UpdateSurface(string underlying, UpdateSurfaceRequest request)
    {
        var snapshot = _store.Get(underlying);

        foreach (var point in snapshot.Points)
        {
            point.Vol = request.Vol;
        }

        _store.SaveAsync(underlying, snapshot);

        Logger.LogInformation($"Updated surface for {underlying}");
    }

    public VolSurfaceSnapshot RefreshSurface(string underlying)
    {
        var snapshot = _store.Get(underlying);
        snapshot.AsOf = DateTime.UtcNow;
        _store.SaveAsync(underlying, snapshot);
        return snapshot;
    }

    public async Task<string> GetUnderlyingsAsync()
    {
        return await _marketDataClient.GetUnderlyingsAsync();
    }
}
