namespace c2030270_saad.Data.Queries.Tenant.Interfaces
{
    using Entities;
    using Resources.Tenant.Request;

    public interface ICreateTenantQuery
    {
        Tenant Execute(Tenant tenant);
    }
}