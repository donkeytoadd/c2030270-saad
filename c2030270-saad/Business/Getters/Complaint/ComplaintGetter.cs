namespace c2030270_saad.Business.Getters.Complaint
{
    using Data.Entities;
    using c2030270_saad.Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintGetter : IComplaintGetter
    {
        private readonly IGetComplaintByIdQuery getComplaintByIdQuery;
        private readonly IGetAllComplaintsByConsumerIdQuery getAllComplaintsByConsumerIdQuery;
        private readonly IGetAllComplaintsByTenantIdQuery getAllComplaintsByTenantIdQuery;
        private readonly IGetAttachmentsByComplaintIdQuery getAttachmentsByComplaintIdQuery;
        
        public ComplaintGetter(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IGetAllComplaintsByConsumerIdQuery getAllComplaintsByConsumerIdQuery,
            IGetAllComplaintsByTenantIdQuery  getAllComplaintsByTenantIdQuery,
            IGetAttachmentsByComplaintIdQuery getAttachmentsByComplaintIdQuery)
        {
            this.getComplaintByIdQuery = getComplaintByIdQuery;
            this.getAllComplaintsByConsumerIdQuery = getAllComplaintsByConsumerIdQuery;
            this.getAllComplaintsByTenantIdQuery =  getAllComplaintsByTenantIdQuery;
            this.getAttachmentsByComplaintIdQuery = getAttachmentsByComplaintIdQuery;
        }
        
        public Complaint GetComplaint(int complaintId, int tenantId)
        {
            var complaint = this.getComplaintByIdQuery.Execute(complaintId, tenantId);

            return complaint ?? new Complaint();
        }

        public List<Complaint> GetComplaintsByConsumerId(int consumerId, int tenantId)
        {
            var complaintList = this.getAllComplaintsByConsumerIdQuery.Execute(consumerId, tenantId);
            return complaintList.Count != 0 ? complaintList : new List<Complaint>();
        }

        public List<Complaint> GetComplaintsByTenantId(int tenantId)
        {
            var complaintList = this.getAllComplaintsByTenantIdQuery.Execute(tenantId);
            return complaintList.Count != 0 ? complaintList : new List<Complaint>(); 
        }

        public List<ComplaintAttachment> GetAttachments(int complaintId)
        {
            var attachmentList = this.getAttachmentsByComplaintIdQuery.Execute(complaintId);
            return attachmentList;
        }
    }
}