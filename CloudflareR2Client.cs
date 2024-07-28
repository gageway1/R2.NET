using R2.NET.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace R2.NET
{
    public class CloudflareR2Client(HttpClient httpClient, IOptions<CloudflareR2Options> options) : ICloudflareR2Client
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly CloudflareR2Options _options = options.Value;

        public async Task<string> UploadBlobAsync(string bucketName, string objectName, Stream data, string contentType)
        {
            var requestUri = $"{_options.ApiBaseUri}/{_options.AccountId}/r2/buckets/{bucketName}/objects/{objectName}";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiToken);
            requestMessage.Content = new StreamContent(data);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return requestUri;
        }

        public async Task<Stream> GetBlobAsync(string bucketName, string objectName)
        {
            var requestUri = $"{_options.ApiBaseUri}/{_options.AccountId}/r2/buckets/{bucketName}/objects/{objectName}";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiToken);

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync();
        }

        public async Task DeleteBlobAsync(string bucketName, string objectName)
        {
            var requestUri = $"{_options.ApiBaseUri}/{_options.AccountId}/r2/buckets/{bucketName}/objects/{objectName}";

            using var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiToken);

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
    }
}
