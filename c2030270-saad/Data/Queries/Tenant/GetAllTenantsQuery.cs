namespace c2030270_saad.Data.Queries.Tenant
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetAllTenantsQuery : IGetAllTenantsQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetAllTenantsQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<Tenant> Execute()
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Tenant.ToList();
            }
        }
    }
}