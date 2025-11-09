namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UpdateComplaintStatusQuery : IUpdateComplaintStatusQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        private readonly ILogger<UpdateComplaintStatusQuery> logger;


        public UpdateComplaintStatusQuery(
            IDbContextFactory<ApplicationDbContext> contextFactory, 
            ILogger<UpdateComplaintStatusQuery> logger)
        {
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        public Complaint Execute(Complaint complaint, string newStatus)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                complaint.Status = newStatus;
                context.SaveChanges();
                return complaint;
            }
        }
    }
}