using System.Collections.Concurrent;
using R2.NET.Configuration;
using Microsoft.Extensions.Options;

namespace R2.NET.Factories
{
    public class CloudflareR2ClientFactory : ICloudflareR2ClientFactory
    {
        private readonly ConcurrentDictionary<string, ICloudflareR2Client> _clientCache;
        private readonly IOptions<CloudflareR2Options> _options;

        public CloudflareR2ClientFactory(
            IOptions<CloudflareR2Options> options)
        {
            _clientCache = new ConcurrentDictionary<string, ICloudflareR2Client>();
            _options = options;
        }

        public ICloudflareR2Client GetClient(string clientName, CancellationToken cancellationToken)
        {
            return _clientCache.GetOrAdd(clientName, _ => CreateClient());
        }

        private CloudflareR2Client CreateClient()
        {
            var httpClient = new HttpClient();
            return new CloudflareR2Client(httpClient, _options);
        }
    }
}