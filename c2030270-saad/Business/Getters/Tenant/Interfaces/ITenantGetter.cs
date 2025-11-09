namespace c2030270_saad.Business.Getters.Tenant.Interfaces
{
    using Data.Entities;
    public interface ITenantGetter
    {
        List<Tenant> GetAllTenants();
        Tenant GetTenantByTenantId(int tenantId);
    }
}