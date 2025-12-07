namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{

    using Entities;

    public interface IGetAllComplaintsByConsumerIdQuery
    {
        List<Complaint> Execute(int consumerId, int tenantId);
    }
}