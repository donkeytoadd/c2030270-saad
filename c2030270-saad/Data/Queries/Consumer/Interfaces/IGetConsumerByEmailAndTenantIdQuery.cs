namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using Entities;

    public interface IGetConsumerByEmailAndTenantIdQuery
    {
        Consumer? Execute(string email, int tenantId);
    }
}