using c2030270_saad.Data.Queries.Consumer.Interfaces;

namespace c2030270_saad.Business.Creators.ConsumerCreator
{
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Resources.Consumer;
    using Consumer.Interfaces;
    using Data.Enums;
    using Data.Queries.Role.Interfaces;

    public class ConsumerCreator : IConsumerCreator
    {
        private readonly IGetConsumerByEmailQuery getConsumerByEmailQuery;
        private readonly ICreateConsumerQuery createConsumerQuery;
        private readonly IGetRoleByRoleNameQuery getRoleByRoleNameQuery;
        
        public ConsumerCreator(
            IGetConsumerByEmailQuery getConsumerByEmailQuery,
            ICreateConsumerQuery createConsumerQuery,
            IGetRoleByRoleNameQuery getRoleByRoleNameQuery)
        {
            this.getConsumerByEmailQuery = getConsumerByEmailQuery;
            this.createConsumerQuery = createConsumerQuery;
            this.getRoleByRoleNameQuery = getRoleByRoleNameQuery;
        }

        public Consumer CreateConsumer(CreateConsumerRequest request)
        {
            var consumerExists = this.getConsumerByEmailQuery.Execute(request.Email);
            var role = this.getRoleByRoleNameQuery.Execute(nameof(RoleEnum.Consumer));
            
            if (consumerExists != null && role != null)
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