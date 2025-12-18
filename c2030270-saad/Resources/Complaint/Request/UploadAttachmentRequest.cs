namespace c2030270_saad.Resources.Complaint.Request
{
    public class UploadAttachmentRequest
    {
        public int ComplaintId { get; set; }
        public int TenantId { get; set; }
        public List<IFormFile> Files { get; set; }
    }

}