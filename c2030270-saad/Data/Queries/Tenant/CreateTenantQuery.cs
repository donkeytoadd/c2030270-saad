namespace c2030270_saad.Data.Queries.Tenant
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CreateTenantQuery : ICreateTenantQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly ILogger<CreateTenantQuery> logger;


        public CreateTenantQuery(
            IDbContextFactory<ApplicationDbContext> contextFactory, 
            ILogger<CreateTenantQuery> logger)
        {
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        public Tenant Execute(Tenant tenant)
        {
            try
            {
                using (var context = contextFactory.CreateDbContext())
                {
                    context.Tenant.Add(tenant);
                    context.SaveChanges();
                    return tenant;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error occured when trying to create tenant");
                throw new Exception("Error occured when trying to create tenant", ex);
            }
        }
    }
}