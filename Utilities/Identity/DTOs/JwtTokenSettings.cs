namespace Utilities.DTOs
{
    public class JwtTokenSettings
    {
        public static string Audience { get; set; }
        public static string Issuer { get; set; }
        public static string SecretKey { get; set; }
        public static string AccessExpiration { get; set; }
        public static string RefreshExpiration { get; set; }
    }
}
