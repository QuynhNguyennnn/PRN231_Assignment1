namespace eStoreAPI.DTOs
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string Roles { get; set; }

    }
}
