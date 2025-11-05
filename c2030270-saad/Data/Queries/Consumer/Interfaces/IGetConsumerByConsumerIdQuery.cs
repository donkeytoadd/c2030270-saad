namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using Entities;

    public interface IGetConsumerByConsumerIdQuery
    {
        Consumer? Execute(int consumerId);
    }
}