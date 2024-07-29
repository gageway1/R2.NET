using System.Collections.Concurrent;
using R2.NET.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace R2.NET.Factories
{
    public class CloudflareR2ClientFactory(
        IOptions<CloudflareR2Options> options,
        ILogger<CloudflareR2ClientFactory> logger,
        ILogger<CloudflareR2Client> clientLogger) : ICloudflareR2ClientFactory
    {
        private readonly ConcurrentDictionary<string, ICloudflareR2Client> _clientCache = new ConcurrentDictionary<string, ICloudflareR2Client>();
        private readonly IOptions<CloudflareR2Options> _options = options;
        private readonly ILogger<CloudflareR2ClientFactory> _logger = logger;
        private readonly ILogger<CloudflareR2Client> _clientLogger = clientLogger;

        public ICloudflareR2Client GetClient(string clientName, CancellationToken cancellationToken)
        {
            try
            {
                return _clientCache.GetOrAdd(clientName, _ => CreateClient(clientName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating CloudflareR2Client for {ClientName}", clientName);
                throw;
            }
        }

        public bool TryRemoveClient(string clientName)
        {
            return _clientCache.TryRemove(clientName, out _);
        }

        public ICloudflareR2Client RefreshClient(string clientName, CancellationToken cancellationToken)
        {
            try
            {
                _clientCache.TryRemove(clientName, out _);
                return _clientCache.GetOrAdd(clientName, _ => CreateClient(clientName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing CloudflareR2Client for {ClientName}", clientName);
                throw;
            }
        }

        private CloudflareR2Client CreateClient(string clientName)
        {
            ValidateConfiguration(_options.Value);

            _logger.LogInformation("Creating CloudflareR2Client for {ClientName}", clientName);
            return new CloudflareR2Client(clientName, _options, _clientLogger);
        }

        private static void ValidateConfiguration(CloudflareR2Options options)
        {
            if (string.IsNullOrEmpty(options.ApiToken) ||
                string.IsNullOrEmpty(options.AccessKeyId) ||
                string.IsNullOrEmpty(options.ApiBaseUri) ||
                string.IsNullOrEmpty(options.Secret))
            {
                throw new InvalidOperationException("Invalid Cloudflare R2 configuration.");
            }
        }
    }
}