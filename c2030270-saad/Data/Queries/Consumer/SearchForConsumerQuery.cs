namespace c2030270_saad.Data.Queries.Consumer
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Resources.Consumer;

    public class SearchForConsumerQuery : ISearchForConsumerQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public SearchForConsumerQuery(IDbContextFactory<ApplicationDbContext> factory)
        {
            contextFactory = factory;
        }

        public List<SearchConsumerResult> Execute(string query)
        {
            using (var context = contextFactory.CreateDbContext())
            {
                query = query.ToLower().Trim();

                return context.Consumer
                    .Where(c =>
                        c.FName.ToLower().Contains(query) ||
                        c.LName.ToLower().Contains(query) ||
                        c.Email.ToLower().Contains(query))
                    .Select(c => new SearchConsumerResult
                    {
                        ConsumerId = c.ConsumerId,
                        FName = c.FName,
                        LName = c.LName,
                        Email = c.Email
                    })
                    .Take(20)
                    .ToList();
            }
        }
    }
}