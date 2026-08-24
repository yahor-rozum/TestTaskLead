namespace VolSurface.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using VolSurface.API.Models;
using VolSurface.API.Services;

[ApiController]
[Route("api/[controller]")]
public class VolSurfaceController : ControllerBase
{
    private readonly VolSurfaceService _service = new VolSurfaceService();

    [HttpGet("GetSurface/{underlying}")]
    public IActionResult GetSurface(string underlying)
    {
        var snapshot = _service.GetSurface(underlying);
        return Ok(snapshot);
    }

    [HttpPost("UpdateSurface/{underlying}")]
    public IActionResult UpdateSurface(string underlying, [FromBody] UpdateSurfaceRequest request)
    {
        _service.UpdateSurface(underlying, request);
        return Ok();
    }

    [HttpGet("GetUnderlyings")]
    public async Task<IActionResult> GetUnderlyings()
    {
        var underlyings = await _service.GetUnderlyingsAsync();
        return Ok(underlyings);
    }

    [HttpPost("RefreshAll")]
    public async Task<IActionResult> RefreshAll()
    {
        var underlyingsCsv = await _service.GetUnderlyingsAsync();
        var names = (underlyingsCsv ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

        var refreshed = new List<string>();

        Parallel.ForEach(names, name =>
        {
            var snapshot = _service.RefreshSurface(name);
            refreshed.Add(snapshot.Underlying);
            LogRefreshAudit(name);
        });

        return Ok(refreshed);
    }

    private static async void LogRefreshAudit(string underlying)
    {
        await Task.Delay(1);
        var tag = underlying.Substring(0, 3).ToUpperInvariant();
        Console.WriteLine($"[{tag}] refreshed at {DateTime.UtcNow:O}");
    }
}
