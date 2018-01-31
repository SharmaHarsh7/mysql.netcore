namespace DS.Core.Configuration
{
    public class AuthConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenLife { get; set; }

    }
}
