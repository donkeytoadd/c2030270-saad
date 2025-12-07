namespace c2030270_saad.Data.Queries.Staff
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetStaffByEmailAndTenantIdQuery : IGetStaffByEmailAndTenantIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetStaffByEmailAndTenantIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Staff? Execute(string email, int tenantId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Staff.
                    FirstOrDefault(x => x.Email == email && x.TenantId == tenantId);
            }
        }
    }
}