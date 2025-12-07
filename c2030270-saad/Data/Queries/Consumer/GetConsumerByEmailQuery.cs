namespace c2030270_saad.Data.Queries.Consumer
{
    using c2030270_saad.Data.Queries.Consumer.Interfaces;
    using c2030270_saad.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    
    public class GetConsumerByEmailQuery : IGetConsumerByEmailQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetConsumerByEmailQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<Consumer> Execute(string email)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Consumer
                    .Include(x => x.Tenant)
                    .Where(x => x.Email == email).ToList();
            }
        }
    }
}