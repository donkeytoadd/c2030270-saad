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
        
        public ComplaintCreator(
            ICreateComplaintQuery createComplaintQuery,
            IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery,
            IComplaintStatusUpdater complaintStatusUpdater)
        {
            this.createComplaintQuery = createComplaintQuery;
            this.getConsumerByConsumerIdQuery = getConsumerByConsumerIdQuery;
            this.complaintStatusUpdater = complaintStatusUpdater;
        }
        
        public Complaint? CreateComplaint(CreateComplaintRequest complaintRequest, int changedById)
        {
            var consumer = this.getConsumerByConsumerIdQuery.Execute(complaintRequest.ConsumerId);
            
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
            
            this.complaintStatusUpdater.UpdateComplaintStatus(complaint.ComplaintId, complaint.Status, changedById);
            
            return complaint;
        }
    }
}