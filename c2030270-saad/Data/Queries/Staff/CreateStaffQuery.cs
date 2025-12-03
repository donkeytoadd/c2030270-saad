namespace c2030270_saad.Data.Queries.Staff
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CreateStaffQuery : ICreateStaffQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public CreateStaffQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Staff Execute(Staff staff)
        {
            try
            {
                using (var context = this.contextFactory.CreateDbContext())
                {
                    context.Staff.Add(staff);
                    context.SaveChanges();

                    return staff;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}