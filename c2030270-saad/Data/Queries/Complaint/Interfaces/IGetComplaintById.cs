namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IGetComplaintById
    {
        Complaint? Execute(int complaintID);
    }
}