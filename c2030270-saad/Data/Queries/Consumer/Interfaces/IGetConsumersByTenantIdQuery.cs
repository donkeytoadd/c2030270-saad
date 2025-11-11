namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using Entities;

    public interface IGetConsumersByTenantIdQuery
    {
        List<Consumer> Execute(int tenantId);
    }
}