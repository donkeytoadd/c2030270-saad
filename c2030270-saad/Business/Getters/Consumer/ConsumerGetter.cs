namespace c2030270_saad.Business.Getters.Consumer
{
    using Data.Entities;
    using Data.Queries.Consumer.Interfaces;
    using Interfaces;

    public class ConsumerGetter : IConsumerGetter
    {
        private readonly IGetConsumersByTenantIdQuery getConsumersByTenantIdQuery;

        public ConsumerGetter(IGetConsumersByTenantIdQuery getConsumersByTenantIdQuery)
        {
            this.getConsumersByTenantIdQuery = getConsumersByTenantIdQuery;
        }
        
        public List<Consumer> GetConsumersByTenantId(int tenantId)
        {
            var consumerList = this.getConsumersByTenantIdQuery.Execute(tenantId);
            return consumerList.Count != 0 ? consumerList : new List<Consumer>(); 
        }
    }
}