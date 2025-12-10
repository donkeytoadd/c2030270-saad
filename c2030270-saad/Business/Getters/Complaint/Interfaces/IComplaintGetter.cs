namespace c2030270_saad.Business.Getters.Complaint.Interfaces
{
    using c2030270_saad.Data.Entities;

    public interface IComplaintGetter
    {
        Complaint? GetComplaint(int complaintId, int  tenantId);
        List<Complaint> GetComplaintsByConsumerId(int consumerId, int tenantId);
        List<Complaint> GetComplaintsByTenantId(int tenantId);
        List<ComplaintAttachment> GetAttachments(int complaintId);
        List<Complaint> GetComplaintsByAssignedToId(int assignedToId, int tenantId);
    }
}