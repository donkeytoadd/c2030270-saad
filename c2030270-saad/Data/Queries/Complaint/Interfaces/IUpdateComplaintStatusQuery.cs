namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IUpdateComplaintStatusQuery
    {
        Complaint Execute(Complaint complaint, string newStatus);
    }
}