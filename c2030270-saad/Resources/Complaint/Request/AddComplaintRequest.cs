namespace c2030270_saad.Resources.Complaint.Request
{

    public class AddComplaintRequest
    {
        public int ConsumerId { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}