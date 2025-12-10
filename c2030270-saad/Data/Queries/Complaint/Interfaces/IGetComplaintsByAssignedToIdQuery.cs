namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IGetComplaintsByAssignedToIdQuery
    {
        List<Complaint> Execute(int assignedToId, int tenantId);
    }
}