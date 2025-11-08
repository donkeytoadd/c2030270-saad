namespace c2030270_saad.Data.Queries.Tenant.Interfaces
{
    using Entities;

    public interface IGetTenantByTenantIdQuery
    {
        Tenant? Execute(int tenantId);
    }
}