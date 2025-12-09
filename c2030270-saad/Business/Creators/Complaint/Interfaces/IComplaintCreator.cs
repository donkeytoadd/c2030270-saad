namespace c2030270_saad.Business.Creators.Complaint.Interfaces
{
    using Data.Entities;
    using Resources.Complaint.Request;

    public interface IComplaintCreator
    {
        Complaint? CreateComplaint(CreateComplaintRequest createComplaint, int changedById, int tenantId);
        ComplaintAttachment SaveAttachment(int complaintId, int tenantId, IFormFile file);
    }
}