namespace c2030270_saad.Business.Getters.Tenant
{
    using Data.Entities;
    using Data.Queries.Tenant.Interfaces;
    using Interfaces;

    public class TenantGetter : ITenantGetter
    {
        private readonly IGetAllTenantsQuery getAllTenantsQuery;
        private readonly IGetTenantByTenantIdQuery getTenantByTenantIdQuery;
        
        public TenantGetter(
            IGetAllTenantsQuery getAllTenantsQuery,
            IGetTenantByTenantIdQuery getTenantByTenantIdQuery)
        {
            this.getAllTenantsQuery = getAllTenantsQuery;
            this.getTenantByTenantIdQuery = getTenantByTenantIdQuery;
        }

        public List<Tenant> GetAllTenants()
        {
            var tenantList = this.getAllTenantsQuery.Execute();
            return tenantList.Count != 0 ? tenantList : new List<Tenant>();
        }

        public Tenant GetTenantByTenantId(int tenantId)
        {
            var tenant =  this.getTenantByTenantIdQuery.Execute(tenantId);
            return tenant ?? new Tenant();
        }
    }
}