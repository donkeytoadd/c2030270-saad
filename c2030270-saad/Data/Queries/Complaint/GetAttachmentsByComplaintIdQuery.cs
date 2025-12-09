namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class GetAttachmentsByComplaintIdQuery : IGetAttachmentsByComplaintIdQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public GetAttachmentsByComplaintIdQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        
        public List<ComplaintAttachment> Execute(int complaintId)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                return context.ComplaintAttachment
                    .Where(x => x.ComplaintId == complaintId)
                    .ToList();
            }
        }
    }
}