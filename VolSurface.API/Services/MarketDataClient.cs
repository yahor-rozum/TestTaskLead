namespace VolSurface.API.Services;

using System.Text.Json;
using VolSurface.API.Ignore;

public class MarketDataClient
{
    private readonly HttpClient _httpClient;

    public MarketDataClient()
    {
        _httpClient = new HttpClient(new FakeUpstreamHandler())
        {
            BaseAddress = new Uri("https://upstream.internal/marketdata/")
        };
    }

    public Task<string> GetUnderlyingsAsync()
    {
        try
        {
            var response = _httpClient.GetAsync("underlyings").Result;
            var stream = response.Content.ReadAsStream();
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var underlyings = JsonSerializer.Deserialize<List<string>>(json);
            return Task.FromResult(string.Join(",", underlyings!));
        }
        catch (Exception ex)
        {
            return Task.FromResult<string>(null!);
        }
    }
}
