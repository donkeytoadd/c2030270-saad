namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IGetComplaintsByTenantIdQuery
    {
        List<Complaint> Execute(int tenantId);
    }
}