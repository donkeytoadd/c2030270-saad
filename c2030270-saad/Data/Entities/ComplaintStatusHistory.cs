namespace c2030270_saad.Data.Entities
{
    public class ComplaintStatusHistory
    {
        public int ComplaintStatusHistoryId { get; set; }
        public int ComplaintId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedById { get; set; }
    }
}