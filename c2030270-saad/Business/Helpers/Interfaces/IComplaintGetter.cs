namespace c2030270_saad.Business.Helpers.Interfaces
{
    using Data.Entities;

    public interface IComplaintGetter
    {
        Complaint? GetComplaint(int complaintId);
    }
}