namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetComplaintByIdQuery : IGetComplaintByIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetComplaintByIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Complaint? Execute(int complaintId, int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Complaint.FirstOrDefault(x => x.ComplaintId == complaintId && x.TenantId == tenantId);                
            }
        }
    }
}