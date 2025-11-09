namespace c2030270_saad.Business.Creators.Tenant
{
    using Data.Entities;
    using Data.Queries.Tenant.Interfaces;
    using Interfaces;
    using Resources.Tenant.Request;

    public class TenantCreator : ITenantCreator
    {
        private readonly ICreateTenantQuery createTenantQuery;
        
        public TenantCreator(ICreateTenantQuery createTenantQuery)
        {
            this.createTenantQuery = createTenantQuery;
        }

        public Tenant CreateTenant(CreateTenantRequest createTenantRequest)
        {
            var mappedTenant = new Tenant
            {
                Name = createTenantRequest.Name,
                Address = createTenantRequest.Address,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            
            var tenant =  this.createTenantQuery.Execute(mappedTenant);
            return tenant;
        }
    }
}