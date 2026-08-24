// Do not edit anything in this folder — see _README.txt.
// Stands in for the real upstream market data HTTP dependency.

namespace VolSurface.API.Ignore;

using System.Net;
using System.Text;

public class FakeUpstreamHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri?.AbsolutePath.EndsWith("underlyings") == true)
        {
            const string json = "[\"EURUSD\",\"USDJPY\",\"EURGBP\"]";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);
        }

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
    }
}
