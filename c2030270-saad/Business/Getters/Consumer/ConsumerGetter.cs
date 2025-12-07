namespace c2030270_saad.Business.Getters.Consumer
{
    using Data.Entities;
    using Data.Queries.Consumer.Interfaces;
    using Interfaces;

    public class ConsumerGetter : IConsumerGetter
    {
        private readonly IGetConsumersByTenantIdQuery getConsumersByTenantIdQuery;
        private readonly IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery;
        private readonly IGetConsumerByEmailQuery getConsumerByEmailQuery;

        public ConsumerGetter(
            IGetConsumersByTenantIdQuery getConsumersByTenantIdQuery,
            IGetConsumerByConsumerIdQuery getConsumerByConsumerIdQuery,
            IGetConsumerByEmailQuery getConsumerByEmailQuery)
        {
            this.getConsumersByTenantIdQuery = getConsumersByTenantIdQuery;
            this.getConsumerByConsumerIdQuery = getConsumerByConsumerIdQuery;
            this.getConsumerByEmailQuery = getConsumerByEmailQuery;
        }
        
        public List<Consumer> GetConsumersByTenantId(int tenantId)
        {
            var consumerList = this.getConsumersByTenantIdQuery.Execute(tenantId);
            return consumerList.Count != 0 ? consumerList : new List<Consumer>(); 
        }

        public Consumer GetConsumerByConsumerId(int consumerId, int tenantId)
        {
            var consumer = this.getConsumerByConsumerIdQuery.Execute(consumerId, tenantId);
            return consumer ?? new Consumer();
        }

        public List<Consumer> GetConsumerByEmail(string email)
        {
            var consumer = this.getConsumerByEmailQuery.Execute(email);
            return consumer;
        }
    }
}