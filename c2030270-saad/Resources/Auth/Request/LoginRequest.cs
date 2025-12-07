namespace c2030270_saad.Resources.Auth.Request
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? TenantId { get; set; }
    }
}