namespace c2030270_saad.Resources.Staff.Request
{
    public class CreateStaffRequest
    {
        public int TenantId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
    }
}