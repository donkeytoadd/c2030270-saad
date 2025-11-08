namespace c2030270_saad.Data.Queries.Tenant
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetTenantByTenantIdQuery : IGetTenantByTenantIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetTenantByTenantIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Tenant? Execute(int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Tenant.FirstOrDefault(x => x.TenantId == tenantId);
            }
        }
    }
}