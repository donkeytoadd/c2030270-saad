namespace c2030270_saad.Data.Queries.Complaint
{
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CreateComplaintAttachmentQuery : ICreateComplaintAttachmentQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public CreateComplaintAttachmentQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public ComplaintAttachment Execute(ComplaintAttachment complaintAttachment)
        {
            using (var context = this.contextFactory.CreateDbContext())
            { 
                context.ComplaintAttachment.Add(complaintAttachment);
                var complaint = context.Complaint.FirstOrDefault(x=>x.ComplaintId == complaintAttachment.ComplaintId);
                
                complaint.UpdatedAt = DateTime.Now;
                context.SaveChanges();
                
                return complaintAttachment;
            }
        }
    }
}