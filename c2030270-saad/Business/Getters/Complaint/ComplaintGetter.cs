namespace c2030270_saad.Business.Getters.Complaint
{
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintGetter : IComplaintGetter
    {
        private readonly IGetComplaintByIdQuery getComplaintByIdQuery;
        private readonly IGetAllComplaintsByConsumerIdQuery getAllComplaintsByConsumerIdQuery;
        private readonly IGetAllComplaintsByTenantIdQuery getAllComplaintsByTenantIdQuery;
        
        public ComplaintGetter(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IGetAllComplaintsByConsumerIdQuery getAllComplaintsByConsumerIdQuery,
            IGetAllComplaintsByTenantIdQuery  getAllComplaintsByTenantIdQuery)
        {
            this.getComplaintByIdQuery = getComplaintByIdQuery;
            this.getAllComplaintsByConsumerIdQuery = getAllComplaintsByConsumerIdQuery;
            this.getAllComplaintsByTenantIdQuery =  getAllComplaintsByTenantIdQuery;
        }
        
        public Complaint? GetComplaint(int complaintId)
        {
            var complaint = this.getComplaintByIdQuery.Execute(complaintId);

            return complaint ?? new Complaint();
        }

        public List<Complaint> GetAllComplaints(int consumerId)
        {
            var complaintList = this.getAllComplaintsByConsumerIdQuery.Execute(consumerId);
            return complaintList.Count != 0 ? complaintList : new List<Complaint>();
        }

        public List<Complaint> GetAllComplaintsByTenantId(int tenantId)
        {
            var complaintList = this.getAllComplaintsByTenantIdQuery.Execute(tenantId);
            return complaintList.Count != 0 ? complaintList : new List<Complaint>(); 
        }
    }
}