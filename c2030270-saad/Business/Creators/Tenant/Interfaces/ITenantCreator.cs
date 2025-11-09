namespace c2030270_saad.Business.Creators.Tenant.Interfaces
{
    using Data.Entities;
    using Resources.Tenant.Request;

    public interface ITenantCreator
    {
        Tenant CreateTenant(CreateTenantRequest createTenantRequest);
    }
}