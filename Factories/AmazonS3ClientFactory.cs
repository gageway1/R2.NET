using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Options;
using R2.NET.Configuration;
using System.Collections.Concurrent;

namespace R2.NET.Factories
{
    internal class AmazonS3ClientFactory(
        IOptions<CloudflareR2Options> options) : IAmazonS3ClientFactory
    {
        private readonly ConcurrentDictionary<string, IAmazonS3> _clientCache = new();
        private readonly IOptions<CloudflareR2Options> _options = options;

        public IAmazonS3 GetClient(string clientName, CancellationToken cancellationToken)
        {
            return _clientCache.GetOrAdd(clientName, _ => CreateClient(_options));
        }

        private static AmazonS3Client CreateClient(IOptions<CloudflareR2Options> options)
        {
            AWSConfigsS3.UseSignatureVersion4 = true;
            var config = new AmazonS3Config
            {
                ServiceURL = options.Value.ApiBaseUri,
                ForcePathStyle = true,
                SignatureVersion = "v4"
            };
            return new AmazonS3Client(options.Value.AccessKeyId, options.Value.Secret, config);
        }
    }
}
