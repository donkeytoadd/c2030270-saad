namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CreateComplaintQuery : ICreateComplaintQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CreateComplaintQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Complaint Execute(Complaint complaint)
        {
            try
            {
                using (var context = this.contextFactory.CreateDbContext())
                {
                    context.Complaint.Add(complaint);
                    context.SaveChanges();

                    return complaint;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}