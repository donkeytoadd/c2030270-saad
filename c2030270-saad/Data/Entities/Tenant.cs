namespace c2030270_saad.Data.Entities
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}