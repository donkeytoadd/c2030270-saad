namespace c2030270_saad.Data.Queries.Staff.Interfaces
{
    using Entities;

    public interface IGetStaffByEmailAndTenantIdQuery
    {
        Staff? Execute(string email, int tenantId);
    }
}