namespace c2030270_saad.Data.Queries.Consumer
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetConsumersByTenantIdQuery : IGetConsumersByTenantIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetConsumersByTenantIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<Consumer> Execute(int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Consumer.Where(c => c.TenantId == tenantId).ToList();
            }
        }
    }
}