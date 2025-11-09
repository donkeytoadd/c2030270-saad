namespace c2030270_saad.Data.Queries.Tenant.Interfaces
{
    using Entities;

    public interface IGetAllTenantsQuery
    {
        List<Tenant> Execute();
    }
}