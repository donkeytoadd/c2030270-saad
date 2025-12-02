namespace c2030270_saad.Business.Creators.Complaint
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Data.Queries.Consumer.Interfaces;
    using Interfaces;
    using Resources.Complaint.Request;

    public class ComplaintCreator : IComplaintCreator
    {
        private readonly ICreateComplaintQuery createComplaintQuery;
        private readonly IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery;
        
        public ComplaintCreator(
            ICreateComplaintQuery createComplaintQuery,
            IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery)
        {
            this.createComplaintQuery = createComplaintQuery;
            this.getConsumerByConsumerIdQuery = getConsumerByConsumerIdQuery;
        }
        
        public Complaint? CreateComplaint(CreateComplaintRequest complaintRequest)
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
            
            return complaint;
        }
    }
}