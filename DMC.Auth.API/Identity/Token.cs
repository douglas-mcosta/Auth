namespace DMC.Auth.API.Identity
{
    public class Token
    {
        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}