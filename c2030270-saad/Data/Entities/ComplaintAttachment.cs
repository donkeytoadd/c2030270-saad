namespace c2030270_saad.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ComplaintAttachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public int ComplaintId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}