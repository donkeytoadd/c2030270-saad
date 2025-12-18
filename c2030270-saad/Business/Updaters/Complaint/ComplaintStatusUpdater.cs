namespace c2030270_saad.Business.Updaters.Complaint
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Interfaces;
    using Resources.Complaint.Request;

    public class ComplaintStatusUpdater : IComplaintStatusUpdater
    {
        private readonly IGetComplaintByIdQuery getComplaintByIdQuery;
        private readonly IUpdateComplaintStatusQuery updateComplaintStatusQuery;
        private readonly ICreateComplaintStatusHistoryQuery createComplaintStatusHistoryQuery;
        
        public ComplaintStatusUpdater(
            IGetComplaintByIdQuery getComplaintByIdQuery,
            IUpdateComplaintStatusQuery  updateComplaintStatusQuery,
            ICreateComplaintStatusHistoryQuery createComplaintStatusHistoryQuery) 
        {
            this.getComplaintByIdQuery = getComplaintByIdQuery;
            this.updateComplaintStatusQuery = updateComplaintStatusQuery;
            this.createComplaintStatusHistoryQuery = createComplaintStatusHistoryQuery;
        }

        public Complaint UpdateComplaintStatus(UpdateComplaintStatusRequest request, int tenantId, int changedById)
        {
            var complaint = this.getComplaintByIdQuery.Execute(request.ComplaintId,tenantId);

            if (complaint == null)
                throw new Exception("Complaint not found.");

            var complaintStatusHistory = new ComplaintStatusHistory()
            {
                ComplaintId = complaint.ComplaintId,
                OldStatus = complaint.Status,
                NewStatus = request.NewStatus,
                ChangedAt = DateTime.Now,
                ChangedById = changedById
            };

            var updatedComplaint = this.updateComplaintStatusQuery.Execute(complaint, request.NewStatus, request.Notes);
            this.createComplaintStatusHistoryQuery.Execute(complaintStatusHistory);

            return updatedComplaint;
        }
    }
}