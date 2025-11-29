namespace c2030270_saad.Data.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public int RoleId { get; set; }
        public int TenantId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Role Role { get; set; }
    }
}