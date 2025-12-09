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
        
        public List<Complaint> Execute(int consumerId, int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Complaint
                    .Include(x=>x.ComplaintAttachments)
                    .Where(x=>x.ConsumerId == consumerId && x.TenantId == tenantId)
                    .OrderByDescending(x=>x.CreatedAt)
                    .ToList();             
            }
        }
    }
}