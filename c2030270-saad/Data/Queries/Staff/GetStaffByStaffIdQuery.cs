namespace c2030270_saad.Data.Queries.Staff
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetStaffByStaffIdQuery :IGetStaffByStaffIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetStaffByStaffIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Staff Execute(int staffId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.Staff.Include(s=>s.Role).FirstOrDefault(x => x.StaffId == staffId) ?? throw new InvalidOperationException();
            }
        }
    }
}