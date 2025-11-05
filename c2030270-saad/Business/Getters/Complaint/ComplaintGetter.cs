namespace c2030270_saad.Business.Getters.Complaint
{
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintGetter : IComplaintGetter
    {
        private readonly IGetComplaintByIdQuery _getComplaintByIdQuery;
        private readonly IGetAllComplaintsByConsumerIdQuery _getAllComplaintsByConsumerIdQuery;
        
        public ComplaintGetter(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IGetAllComplaintsByConsumerIdQuery getAllComplaintsByConsumerIdQuery)
        {
            this._getComplaintByIdQuery = getComplaintByIdQuery;
            this._getAllComplaintsByConsumerIdQuery = getAllComplaintsByConsumerIdQuery;
        }
        
        public Complaint? GetComplaint(int complaintId)
        {
            var complaint = this._getComplaintByIdQuery.Execute(complaintId);

            return complaint ?? new Complaint();
        }

        public List<Complaint> GetAllComplaints(int consumerId)
        {
            var complaintList = this._getAllComplaintsByConsumerIdQuery.Execute(consumerId);
            return complaintList.Count != 0 ? complaintList : new List<Complaint>();
        }
    }
}