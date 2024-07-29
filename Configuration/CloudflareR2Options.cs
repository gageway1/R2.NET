namespace R2.NET.Configuration
{
    public class CloudflareR2Options
    {
        public const string SettingsName = "CloudflareR2";

        public string ApiBaseUri { get; set; } = string.Empty;
        public string ApiToken { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string AccessKeyId { get; set; } = string.Empty;
        public double? PresignedUrlExpiryInMinutes { get; set; } = 15;
    }
}
