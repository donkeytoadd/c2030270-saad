namespace c2030270_saad.Business.Updaters.Complaint.Interfaces
{
    using Data.Entities;
    using Resources.Complaint.Request;

    public interface IComplaintStatusUpdater
    {
        Complaint UpdateComplaintStatus(UpdateComplaintStatusRequest request, int tenantId,int changedById);
    }
}