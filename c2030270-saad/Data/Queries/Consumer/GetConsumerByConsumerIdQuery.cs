namespace c2030270_saad.Data.Queries.Consumer
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetConsumerByConsumerIdQuery : IGetConsumerByConsumerIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetConsumerByConsumerIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Consumer? Execute(int consumerId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Consumer.FirstOrDefault(x => x.ConsumerId == consumerId);
            }
        }
    }
}