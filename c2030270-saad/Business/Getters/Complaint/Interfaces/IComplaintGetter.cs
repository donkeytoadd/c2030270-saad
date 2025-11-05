namespace c2030270_saad.Business.Getters.Complaint.Interfaces
{
    using c2030270_saad.Data.Entities;

    public interface IComplaintGetter
    {
        Complaint? GetComplaint(int complaintId);
        List<Complaint> GetAllComplaints(int consumerId);
    }
}