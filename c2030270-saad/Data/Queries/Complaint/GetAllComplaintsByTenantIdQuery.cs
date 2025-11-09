namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetAllComplaintsByTenantIdQuery : IGetAllComplaintsByTenantIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetAllComplaintsByTenantIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<Complaint> Execute(int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Complaint.Where(x=>x.TenantId == tenantId).ToList();             
            }
        }
    }
}