namespace c2030270_saad.Business.Creators.Complaint
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Data.Queries.Consumer.Interfaces;
    using Interfaces;
    using Resources.Complaint.Request;
    using Updaters.Complaint.Interfaces;

    public class ComplaintCreator : IComplaintCreator
    {
        private readonly ICreateComplaintQuery createComplaintQuery;
        private readonly IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery;
        private readonly IComplaintStatusUpdater complaintStatusUpdater;
        private readonly ICreateComplaintAttachmentQuery createComplaintAttachmentQuery;
        
        public ComplaintCreator(
            ICreateComplaintQuery createComplaintQuery,
            IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery,
            IComplaintStatusUpdater complaintStatusUpdater,
            ICreateComplaintAttachmentQuery createComplaintAttachmentQuery)
        {
            this.createComplaintQuery = createComplaintQuery;
            this.getConsumerByConsumerIdQuery = getConsumerByConsumerIdQuery;
            this.complaintStatusUpdater = complaintStatusUpdater;
            this.createComplaintAttachmentQuery = createComplaintAttachmentQuery;
        }
        
        public Complaint? CreateComplaint(CreateComplaintRequest complaintRequest, int changedById, int tenantId)
        {
            try
            {
                var consumer = this.getConsumerByConsumerIdQuery.Execute(complaintRequest.ConsumerId, tenantId);

                if (consumer == null)
                {
                    return null;
                }

                var mappedComplaint = new Complaint
                {
                    ConsumerId = complaintRequest.ConsumerId,
                    TenantId = consumer.TenantId,
                    Status = "Open",
                    Priority = complaintRequest.Priority,
                    Title = complaintRequest.Title,
                    Description = complaintRequest.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null,
                    ResolvedAt = null
                };

                var complaint = this.createComplaintQuery.Execute(mappedComplaint);

                this.complaintStatusUpdater.UpdateComplaintStatus(complaint.ComplaintId, complaint.TenantId,
                    complaint.Status, changedById);

                return complaint;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating complaint: {ex.Message}", ex);
            }
        }

        public ComplaintAttachment SaveAttachment(int complaintId, int tenantId, IFormFile file)
        {
            var uploadsRoot = Path.Combine("wwwroot", "uploads", $"tenant-{tenantId}", $"complaint-{complaintId}");
            
            if (!Directory.Exists(uploadsRoot))
                Directory.CreateDirectory(uploadsRoot);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsRoot, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            var attachment = new ComplaintAttachment
            {
                ComplaintId = complaintId,
                FileName = uniqueFileName,
                OriginalName = file.FileName,
                FilePath = $"/uploads/tenant-{tenantId}/complaint-{complaintId}/{uniqueFileName}",
                UploadedAt = DateTime.UtcNow
            };
            
            this.createComplaintAttachmentQuery.Execute(attachment);

            return attachment;
        }
    }
}