namespace R2.NET.Factories
{
    public interface ICloudflareR2ClientFactory
    {
        ICloudflareR2Client GetClient(string clientName, CancellationToken cancellationToken);
    }
}
