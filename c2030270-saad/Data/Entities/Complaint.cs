namespace c2030270_saad.Data.Entities
{
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public int TenantId { get; set; }
        public int ConsumerId { get; set; }
        public int? AssignedToId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}