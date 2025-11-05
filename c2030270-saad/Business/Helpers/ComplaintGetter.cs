namespace c2030270_saad.Business.Helpers
{
    using Data.Entities;
    using Data.Queries.Complaint.Interfaces;
    using Interfaces;

    public class ComplaintGetter : IComplaintGetter
    {
        private readonly IGetComplaintById getComplaintById;
        
        public ComplaintGetter(
            IGetComplaintById getComplaintById)
        {
            this.getComplaintById = getComplaintById;
        }
        
        public Complaint? GetComplaint(int complaintId)
        {
            return this.getComplaintById.Execute(complaintId);
        }
    }
}