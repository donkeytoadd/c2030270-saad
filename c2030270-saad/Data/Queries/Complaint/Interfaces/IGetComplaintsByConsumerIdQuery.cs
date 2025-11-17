namespace c2030270_saad.Data.Queries.Complaint.Interfaces
{

    using Entities;

    public interface IGetComplaintsByConsumerIdQuery
    {
        List<Complaint> Execute(int consumerId);
    }
}