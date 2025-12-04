namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface ICreateComplaintStatusHistoryQuery
    {
        ComplaintStatusHistory Execute(ComplaintStatusHistory complaintStatusHistory);
    }
}