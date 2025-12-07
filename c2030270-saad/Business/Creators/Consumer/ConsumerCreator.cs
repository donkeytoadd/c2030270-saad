namespace c2030270_saad.Business.Creators.Consumer
{
    using c2030270_saad.Business.Creators.Consumer.Interfaces;
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Data.Enums;
    using c2030270_saad.Data.Queries.Consumer.Interfaces;
    using c2030270_saad.Data.Queries.Role.Interfaces;
    using c2030270_saad.Resources.Consumer;

    public class ConsumerCreator : IConsumerCreator
    {
        private readonly IGetConsumerByEmailAndTenantIdQuery getConsumerByEmailAndTenantIdQuery;
        private readonly ICreateConsumerQuery createConsumerQuery;
        private readonly IGetRoleByRoleNameQuery getRoleByRoleNameQuery;
        
        public ConsumerCreator(
            IGetConsumerByEmailAndTenantIdQuery getConsumerByEmailAndTenantIdQuery,
            ICreateConsumerQuery createConsumerQuery,
            IGetRoleByRoleNameQuery getRoleByRoleNameQuery)
        {
            this.getConsumerByEmailAndTenantIdQuery = getConsumerByEmailAndTenantIdQuery;
            this.createConsumerQuery = createConsumerQuery;
            this.getRoleByRoleNameQuery = getRoleByRoleNameQuery;
        }

        public Consumer CreateConsumer(CreateConsumerRequest request)
        {
            var consumerExists = this.getConsumerByEmailAndTenantIdQuery.Execute(request.Email, request.TenantId);
            var role = this.getRoleByRoleNameQuery.Execute(nameof(RoleEnum.Consumer));
            
            if (consumerExists == null && role != null)
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