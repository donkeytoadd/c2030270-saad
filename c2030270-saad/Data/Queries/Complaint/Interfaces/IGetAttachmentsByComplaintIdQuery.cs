namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IGetAttachmentsByComplaintIdQuery
    {
        List<ComplaintAttachment> Execute(int complaintId);
    }
}