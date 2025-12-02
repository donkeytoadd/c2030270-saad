using c2030270_saad.Data.Queries.Consumer.Interfaces;

namespace c2030270_saad.Business.Creators.ConsumerCreator
{
    using c2030270_saad.Business.Creators.ConsumerCreator.Interfaces;
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Resources.Consumer;
    using Data.Queries.Role.Interfaces;

    public class ConsumerCreator : IConsumerCreator
    {
        private readonly IGetConsumerByConsumerEmailQuery getConsumerByConsumerEmailQuery;
        private readonly ICreateConsumerQuery createConsumerQuery;
        private readonly IGetRoleByRoleNameQuery getRoleByRoleNameQuery;
        
        public ConsumerCreator(
            IGetConsumerByConsumerEmailQuery getConsumerByConsumerEmailQuery,
            ICreateConsumerQuery createConsumerQuery,
            IGetRoleByRoleNameQuery getRoleByRoleNameQuery)
        {
            this.getConsumerByConsumerEmailQuery = getConsumerByConsumerEmailQuery;
            this.createConsumerQuery = createConsumerQuery;
            this.getRoleByRoleNameQuery = getRoleByRoleNameQuery;
        }

        public Consumer CreateConsumer(CreateConsumerRequest request)
        {
            var consumerExists = this.getConsumerByConsumerEmailQuery.Execute(request.Email);
            var role = this.getRoleByRoleNameQuery.Execute("Consumer");
            
            if (!consumerExists && role != null)
            {
                var mappedConsumer = new Consumer()
                {
                    TenantId =  request.TenantId,
                    RoleId = role.RoleId,
                    FName = request.FName,
                    LName = request.LName,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    ContactNumber = request.ContactNumber,
                    IsActive = true,
                    CreatedAt =  DateTime.Now
                };
                
                var consumer = this.createConsumerQuery.Execute(mappedConsumer);
                
                return consumer;
            }

            throw new Exception("Consumer already exists.");
        }
    }
}