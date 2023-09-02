namespace Api.Client.MarquetStore.Security
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; } = false;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = false;
        public string? ValidAudience { get; set; }
        public bool RequiredExpirationTime { get; set; } = true;
        public bool ValidateLifetime { get; set; }
    }
}
