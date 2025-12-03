namespace c2030270_saad.Data.Queries.Staff
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetStaffByStaffEmailQuery : IGetStaffByStaffEmailQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetStaffByStaffEmailQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Staff? Execute(string staffEmail)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Staff.FirstOrDefault(s => s.Email == staffEmail);
            }
        }
    }
}