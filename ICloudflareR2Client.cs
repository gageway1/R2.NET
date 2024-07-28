namespace R2.NET
{
    public interface ICloudflareR2Client
    {
        Task<string> UploadBlobAsync(string bucketName, string objectName, Stream data, string contentType);
        Task<Stream> GetBlobAsync(string bucketName, string objectName);
        Task DeleteBlobAsync(string bucketName, string objectName);
    }
}
