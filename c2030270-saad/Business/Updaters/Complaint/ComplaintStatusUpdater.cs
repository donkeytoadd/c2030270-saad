namespace c2030270_saad.Business.Updaters.Complaint
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintStatusUpdater : IComplaintStatusUpdater
    {
        private readonly IGetComplaintByIdQuery getComplaintByIdQuery;
        private readonly IUpdateComplaintStatusQuery updateComplaintStatusQuery;
        private readonly ICreateComplaintStatusHistoryQuery _createComplaintStatusHistoryQuery;
        
        public ComplaintStatusUpdater(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IUpdateComplaintStatusQuery  updateComplaintStatusQuery,
            ICreateComplaintStatusHistoryQuery createComplaintStatusHistoryQuery) 
        {
            this.getComplaintByIdQuery = getComplaintByIdQuery;
            this.updateComplaintStatusQuery = updateComplaintStatusQuery;
            this._createComplaintStatusHistoryQuery = createComplaintStatusHistoryQuery;
        }

        public Complaint UpdateComplaintStatus(int complaintId, int tenantId,  string newStatus, int changedById)
        {
            var complaint = this.getComplaintByIdQuery.Execute(complaintId,tenantId);

            if (complaint == null)
                throw new Exception("Complaint not found.");

            var complaintStatusHistory = new ComplaintStatusHistory()
            {
                ComplaintId = complaint.ComplaintId,
                OldStatus = complaint.Status,
                NewStatus = newStatus,
                ChangedAt = DateTime.Now,
                ChangedById = changedById
                
            };

            this._createComplaintStatusHistoryQuery.Execute(complaintStatusHistory);

            return this.updateComplaintStatusQuery.Execute(complaint, newStatus);
        }
    }
}