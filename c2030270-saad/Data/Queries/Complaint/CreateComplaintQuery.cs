namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Resources.Complaint.Request;

    public class CreateComplaintQuery : ICreateComplaintQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CreateComplaintQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public Complaint Execute(Complaint complaint)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                context.Complaint.Add(complaint);
                context.SaveChanges();

                return complaint;
            }
        }
    }
}