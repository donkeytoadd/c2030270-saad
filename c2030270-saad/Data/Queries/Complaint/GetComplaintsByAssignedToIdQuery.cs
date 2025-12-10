namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetComplaintsByAssignedToIdQuery : IGetComplaintsByAssignedToIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetComplaintsByAssignedToIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public List<Complaint> Execute(int assignedToId, int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Complaint
                    .Where(c => c.AssignedToId == assignedToId && c.TenantId == tenantId)
                    .ToList();
            }
        }
    }
}