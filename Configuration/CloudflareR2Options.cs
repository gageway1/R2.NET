namespace R2.NET.Configuration
{
    public class CloudflareR2Options
    {
        public const string SettingsName = "CloudflareR2";

        public string ApiBaseUri { get; set; } = "https://api.cloudflare.com/client/v4/accounts";
        public string AccountId { get; set; } = string.Empty;
        public string ApiToken { get; set; } = string.Empty;
    }
}
