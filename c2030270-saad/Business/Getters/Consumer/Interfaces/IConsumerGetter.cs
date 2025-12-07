namespace c2030270_saad.Business.Getters.Consumer.Interfaces
{
    using Data.Entities;

    public interface IConsumerGetter
    {
        List<Consumer> GetConsumersByTenantId(int tenantId);
        Consumer GetConsumerByConsumerId(int consumerId, int tenantId);
        List<Consumer> GetConsumerByEmail(string email);
    }
}