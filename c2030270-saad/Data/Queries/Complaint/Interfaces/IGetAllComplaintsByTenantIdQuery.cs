namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{
    using Entities;

    public interface IGetAllComplaintsByTenantIdQuery
    {
        List<Complaint> Execute(int tenantId);
    }
}