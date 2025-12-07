namespace c2030270_saad.Data.Queries.Consumer
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetConsumerByEmailAndTenantIdQuery : IGetConsumerByEmailAndTenantIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetConsumerByEmailAndTenantIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Consumer? Execute(string email, int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Consumer.
                    FirstOrDefault(x => x.Email == email && x.TenantId == tenantId);
            }
        }
    }
}