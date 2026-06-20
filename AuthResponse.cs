namespace try4.Models
{
    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public required object User { get; set; }
    }
}
