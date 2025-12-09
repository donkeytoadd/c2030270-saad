namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CreateComplaintStatusHistoryQuery : ICreateComplaintStatusHistoryQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CreateComplaintStatusHistoryQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public ComplaintStatusHistory Execute(ComplaintStatusHistory complaintStatusHistory)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {

                context.ComplaintStatusHistory.Add(complaintStatusHistory);
                context.SaveChanges();
                return complaintStatusHistory;
            }
        }
    }
}