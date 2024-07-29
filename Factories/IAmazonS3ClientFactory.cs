using Amazon.S3;

namespace R2.NET.Factories
{
    internal interface IAmazonS3ClientFactory
    {
        IAmazonS3 GetClient(string clientName, CancellationToken cancellationToken);
    }
}
