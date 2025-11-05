namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;
    using Resources.Complaint.Request;

    public interface ICreateComplaintQuery
    {
        Complaint Execute(Complaint complaint);
    }
}