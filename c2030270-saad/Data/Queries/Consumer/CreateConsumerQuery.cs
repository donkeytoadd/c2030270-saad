namespace c2030270_saad.Data.Queries.Consumer
{
    using c2030270_saad.Data.Queries.Consumer.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using c2030270_saad.Data.Entities;

    public class CreateConsumerQuery : ICreateConsumerQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CreateConsumerQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Consumer Execute(Consumer consumer)
        {
            try
            {
                using (var context = this.contextFactory.CreateDbContext())
                {
                    context.Consumer.Add(consumer);
                    context.SaveChanges();

                    return consumer;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}