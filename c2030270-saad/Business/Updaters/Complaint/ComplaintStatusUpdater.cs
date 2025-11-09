namespace c2030270_saad.Business.Updaters.Complaint
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintStatusUpdater : IComplaintStatusUpdater
    {
        private readonly IGetComplaintByIdQuery getComplaintByIdQuery;
        private readonly IUpdateComplaintStatusQuery updateComplaintStatusQuery;
        
        public ComplaintStatusUpdater(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IUpdateComplaintStatusQuery  updateComplaintStatusQuery) 
        {
            this.getComplaintByIdQuery = getComplaintByIdQuery;
            this.updateComplaintStatusQuery = updateComplaintStatusQuery;
        }

        public Complaint UpdateComplaintStatus(int complaintId, string newStatus)
        {
            var complaint = this.getComplaintByIdQuery.Execute(complaintId);
            
            return this.updateComplaintStatusQuery.Execute(complaint!, newStatus);
        }
    }
}