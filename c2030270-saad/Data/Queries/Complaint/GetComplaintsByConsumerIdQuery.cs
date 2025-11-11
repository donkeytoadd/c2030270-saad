namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetComplaintsByConsumerIdQuery : IGetAllComplaintsByConsumerIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetComplaintsByConsumerIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<Complaint> Execute(int consumerId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Complaint.Where(x=>x.ConsumerId == consumerId).ToList();             
            }
        }
    }
}