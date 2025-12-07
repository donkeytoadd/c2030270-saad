namespace c2030270_saad.Business.Updaters.Complaint.Interfaces
{
    using Data.Entities;

    public interface IComplaintStatusUpdater
    {
        Complaint UpdateComplaintStatus(int complaintId, int tenantId, string newStatus, int changedById);
    }
}