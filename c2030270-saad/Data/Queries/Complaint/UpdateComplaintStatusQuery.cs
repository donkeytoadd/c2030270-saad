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

        public Complaint Execute(Complaint complaint, string newStatus, string? notes)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                var complaintToUpdate = context.Complaint.FirstOrDefault(x => x.ComplaintId ==  complaint.ComplaintId);

                if (complaintToUpdate != null)
                {
                    complaintToUpdate.Status = newStatus;
                    complaintToUpdate.UpdatedAt = DateTime.Now;
                    
                    if (notes != null)
                    {
                        complaintToUpdate.ResolutionNotes = notes;
                    }

                    context.SaveChanges();
                    return complaintToUpdate;
                }
                return complaint;
            }
        }
    }
}